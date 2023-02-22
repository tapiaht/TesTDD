using Bogus;
using TesTDD.Domain.Core.Entity;

namespace TesTDD.Infra.Repository.Tests.Fixtures
{
    public class NoticiaFixture
    {
        public Noticia CreateValid()
        {
            return new Faker<Noticia>("es_MX")
                .RuleFor(o => o.Titulo, f => f.Name.JobTitle())
                .RuleFor(o => o.Contenido, f => f.Name.JobDescriptor())
                .Generate();
        }
    }
}
