using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArch.Auth
{
    public static class CleanArchAuthServiceCollectionExtensions
    {
        public static IServiceCollection AddCleanArchAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IMessageService, DummyMessageService>();
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CleanArchIdentityDb")));

            services.AddIdentityCore<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddRoleManager<RoleManager<IdentityRole>>()
                    .AddSignInManager<SignInManager<IdentityUser>>()
                    .AddRoleValidator<RoleValidator<IdentityRole>>()
                    .AddEntityFrameworkStores<AuthDbContext>()
                    .AddDefaultTokenProviders()
                    .AddUserManager<UserManager<IdentityUser>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

                    .AddCookie("Identity.Application")
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            return services;

        }
    }
}
