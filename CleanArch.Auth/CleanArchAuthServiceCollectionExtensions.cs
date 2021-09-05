using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Auth
{
    public static class CleanArchAuthServiceCollectionExtensions
    {
        public static void AddCleanArchAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CleanArchIdentityDb")));
            
            services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AuthDbContext>();
        }
    }
}
