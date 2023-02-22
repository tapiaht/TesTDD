using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TesTDD.Domain.Core.Entity;
using TesTDD.Domain.Core.Interfaces.Repository;
using TesTDD.Infra.Repository;
using System;

namespace TesTDD.Infra.Repository.Tests.Fixtures
{
    public class DependencyInjectionFixture
    {
        public readonly IServiceProvider _ServiceProvider;

        public DependencyInjectionFixture()
        {
            var services = new ServiceCollection();

            services.AddDbContext<RepositoryContext>(options => options.UseInMemoryDatabase(databaseName: "TEMP"));
            services.AddTransient(typeof(GenericRepositoryCrud<,>));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<IUnitOfWork>(o => o.GetService<UnitOfWork>());

            _ServiceProvider = services.BuildServiceProvider();
            SeedRepositoryContext(_ServiceProvider);
        }

        private static void SeedRepositoryContext(IServiceProvider services)
        {
            RepositoryContext context = services.GetService<RepositoryContext>();

            CreateNoticia(context);
            CreateNoticia(context);

            context.SaveChanges();
        }

        private static void CreateNoticia(DbContext context)
        {
            Noticia noticia = new Noticia() { Titulo = "Victor", Contenido = "Fructuoso" };
            context.Set<Noticia>().Add(noticia);
        }
    }
}