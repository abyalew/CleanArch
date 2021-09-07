using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.Ioc
{
    public class Container
    {
        public static void ConfigureServices(IServiceCollection service)
        {
            //Application Layer
            service.AddScoped<ICourseService, CourseService>();


            //Infra.Data Layer

            service.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}
