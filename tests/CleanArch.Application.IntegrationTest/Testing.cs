using CleanArch.Api;
using CleanArch.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.IntegrationTest
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;

        [OneTimeSetUp]
        public void InitialSetup()
        {
            var directory = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables();
            _configuration = builder.Build();

            var services = new ServiceCollection();

            var startup = new Startup(_configuration);

            startup.ConfigureServices(services);

            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            EnsureDatabase();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<UniversityDbContext>();

            context.Database.Migrate();

            _checkpoint = new Checkpoint
            {
                TablesToIgnore = new[] { "__EFMigrationsHistory" }
            };
        }

        private static string GetDbConnectionString()
        {
            return _configuration.GetConnectionString("UniversityDb");
        }

        public static void ResetDatabaseState()
        {
            _checkpoint.Reset(GetDbConnectionString());
        }

        public static T GetService<T>()
        {
            var scope = _scopeFactory.CreateScope();
            return scope.ServiceProvider.GetService<T>();
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
        {
            var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<UniversityDbContext>();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
        {
            var context = GetService<UniversityDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }
    }
}
