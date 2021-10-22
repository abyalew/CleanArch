using CleanArch.Application.Abstractions.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Context;
using CleanArch.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (Convert.ToBoolean(configuration.GetSection("UseInMemoryDatabase").Value))
            {
                services.AddDbContext<UniversityDbContext>(options =>
                    options.UseInMemoryDatabase("UniversityMemoryDb"));
            }
            else
            {
                services.AddDbContext<UniversityDbContext>(options =>
                       options.UseSqlServer(
                           configuration.GetConnectionString("UniversityDb"),
                           b => b.MigrationsAssembly(typeof(UniversityDbContext).Assembly.FullName)));
            }
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IRecipeRepo, RecipeRepo>();


            return services;
        }
    }
}
