using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanArch.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageService _messageService;
        private readonly JwtSettings _jwtSettings;
        public IdentityController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMessageService messageService, JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _messageService = messageService;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("register")]
        public async Task<JsonResult> Register(string email, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "email or password is null"
                });
            }

            if (password != confirmPassword)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "Passwords don't match!"
                });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "email address already taken."
                });
            }

            var newUser = new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            IdentityResult userCreationResult = null;
            try
            {
                userCreationResult = await _userManager.CreateAsync(newUser, password);
            }
            catch (SqlException)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.InternalServerError)
                {
                    Message = "Error communicating with the database, see logs for more details"
                });
            }
            user = await _userManager.FindByEmailAsync(email);
            if (!userCreationResult.Succeeded)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "An error occurred when creating the user, see nested errors",
                    Errors = userCreationResult.Errors.Select(x => new Response<string>(HttpStatusCode.BadRequest)
                    {
                        Message = $"[{x.Code}] {x.Description}"
                    })
                });
            }

            //var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            //var param = new
            //{
            //    Id = newUser.Id,
            //    token = emailConfirmationToken
            //};
            //var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", param, Request.Scheme);

            //await _messageService.Send(email, "Verify your email", $"Click <a href=\"{tokenVerificationUrl}\">here</a> to verify your email");

            //return new JsonResult(new Response<User>(HttpStatusCode.OK)
            //{
            //    Message = $"Registration completed, please verify your email - {email}",
            //    Content = new User
            //    {
            //        Id = user.Id,
            //        Email = user.Email,
            //        UserName = user.UserName
            //    }
            //});

            return await Login(email, password);
        }

        [HttpPost]
        [Route("verifyEmail")]
        public async Task<JsonResult> VerifyEmail(string id, string token)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            var emailConfirmationResult = await _userManager.ConfirmEmailAsync(user, token);

            if (!emailConfirmationResult.Succeeded)
            {
                return new JsonResult(new Response<User>(HttpStatusCode.NotFound)
                {
                    Message = "Email verification failed.",
                    Errors = emailConfirmationResult.Errors.Select(x => new Response<string>(HttpStatusCode.BadRequest)
                    {
                        Message = $"[{x.Code}] {x.Description}"
                    })
                });
            }

            return new JsonResult(new Response<User>(HttpStatusCode.OK)
            {
                Content = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            });
        }

        [HttpPost]
        [Route("login")]
        public async Task<JsonResult> Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "email or password is null"
                });
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "Invalid Login and/or password"
                });
            }

            if (!user.EmailConfirmed)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "Email not confirmed, please check your email for confirmation link"
                });
            }

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!userHasValidPassword)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "Invalid Login and/or password"
                });
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, password, isPersistent: true, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                return new JsonResult(new Response<string>(HttpStatusCode.BadRequest)
                {
                    Message = "Invalid Login and/or password"
                });
            }

            JwtSecurityTokenHandler tokenHandler;
            SecurityToken token;
            GenerateUserAccessToken(user, out tokenHandler, out token);

            return new JsonResult(new Response<string>(HttpStatusCode.OK)
            {
                Content = tokenHandler.WriteToken(token)
            });
        }

        private void GenerateUserAccessToken(IdentityUser user, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
        {
            tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("Id", user.Id),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_jwtSettings.GetSecretByte()), SecurityAlgorithms.HmacSha256Signature)
            };
            token = tokenHandler.CreateToken(tokenDescriptor);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<JsonResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return new JsonResult(new Response<string>(HttpStatusCode.OK)
            {
                Message = "You have been successfully logged out"
            });
        }

        [HttpGet]
        [Authorize]
        [Route("getuserprofile")]
        public async Task<JsonResult> GetUserProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return new JsonResult(new Response<User>(HttpStatusCode.NotFound) { Message = "User not found." });
            return new JsonResult(new Response<User>(HttpStatusCode.OK)
            {
                Content = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName
                }
            });
        }
    }
}
