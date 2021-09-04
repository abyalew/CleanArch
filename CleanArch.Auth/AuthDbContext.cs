using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Auth
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext() : base()
        {

        }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-NIQVEFD\\SQLEXPRESS;Initial Catalog=CleanArchAuthDb;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
