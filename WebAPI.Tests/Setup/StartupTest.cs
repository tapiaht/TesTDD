using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TesTDD.Infra.Repository;
using TesTDD.WebAPI;
using System;

namespace WebAPI.Tests.Setup
{
    public class StartupTest : Startup {
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.BuildServiceProvider().SeedRepositoryContext();
        }

        protected override void RegisterDbContext(IServiceCollection services)
        {
            Guid guid = Guid.NewGuid();
            string databaseNameLoja = $"{guid}";

            services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase(databaseNameLoja));
        }
    }
}
