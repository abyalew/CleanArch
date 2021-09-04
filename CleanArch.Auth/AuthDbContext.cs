using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Auth
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext() : base()
        {

        }
        //public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        //{

        //}
    }
}
