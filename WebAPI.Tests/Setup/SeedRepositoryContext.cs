using Bogus;
using Microsoft.Extensions.DependencyInjection;
using TesTDD.Domain.Core.Entity;
using TesTDD.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Tests.Setup
{
    public static class RepositoryContextExtensions
    {
        public static IServiceProvider SeedRepositoryContext(this IServiceProvider services)
        {
            RepositoryContext context = services.GetService<RepositoryContext>();

            context.SeedNoticia();

            context.SaveChanges();

            return services;
        }

        private static RepositoryContext SeedNoticia(this RepositoryContext context)
        {
            context.Set<Noticia>().Add(CreateNoticia(Guid.Parse("0d46be7a-3417-4a06-adff-3e1090bf4ea9")));
            context.Set<Noticia>().Add(CreateNoticia(Guid.Parse("d13a07df-085d-4830-a26d-976fa06c1074")));
            context.Set<Noticia>().AddRange(CreateNoticias(100));

            return context;
        }

        private static Noticia CreateNoticia(Guid id)
        {
            return new Faker<Noticia>("pt_BR")
                .RuleFor(o => o.Id, id)
                .RuleFor( o => o.Titulo, f => f.Name.JobTitle())
                .RuleFor( o => o.Contenido, f => f.Name.JobDescriptor())
                .Generate();
        }

        private static IEnumerable<Noticia> CreateNoticias(int count)
        {
            return new Faker<Noticia>("pt_BR")
                .RuleFor(o => o.Id, f => f.Random.Guid())
                .RuleFor( o => o.Titulo, f => f.Name.JobTitle())
                .RuleFor( o => o.Contenido, f => f.Name.JobDescriptor())
                .Generate(count);
        }
    }
}
