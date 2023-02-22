using Microsoft.Extensions.DependencyInjection;
using TesTDD.Domain.Core.Entity;
using TesTDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TesTDD.Infra.Repository.Tests
{
    public class RepositoryContextTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Noticia> _Repository;
        private readonly UnitOfWork _UnitOfWork;

        public RepositoryContextTests(DependencyInjectionFixture fixture)
        {
            IServiceProvider serviceProvider = fixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Noticia>>();
            _UnitOfWork = serviceProvider.GetService<UnitOfWork>();
        }

        [Fact(DisplayName = "DADA una Noticia CUANDO ha sido recuperada y modificada 2 veces ENTONCES considere la segunda modificación al confirmar")]
        public async Task CommitWithoutChanges()
        {
            //Arrange
            Guid id = _Repository.GetAll().First().Id;

            //Act
            Noticia noticia1 = await _Repository.GetAsync(id);
            noticia1.Titulo = "First Change";
            await _Repository.UpdateAsync(noticia1);
            
            Noticia noticia2 = await _Repository.GetAsync(id);
            noticia2.Titulo = "Second Change";
            await _Repository.UpdateAsync(noticia2);

            await _UnitOfWork.CommitAsync();
            Noticia noticia = await _Repository.GetAsync(id);

            //Assert
            Assert.Equal(noticia2.Titulo, noticia.Titulo);
        }
    }
}
