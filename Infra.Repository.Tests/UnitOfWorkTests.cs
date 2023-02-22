using Microsoft.Extensions.DependencyInjection;
using TesTDD.Domain.Core.Entity;
using TesTDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TesTDD.Infra.Repository.Tests
{
    public class UnitOfWorkTests : IClassFixture<DependencyInjectionFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Noticia> _Repository;
        private readonly UnitOfWork _UnitOfWork;

        public UnitOfWorkTests(DependencyInjectionFixture fixture)
        {
            IServiceProvider serviceProvider = fixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Noticia>>();
            _UnitOfWork = serviceProvider.GetService<UnitOfWork>();
        }

        [Fact(DisplayName = "DADA una nueva Noticia que persiste CUANDO se realiza la confirmación ENTONCES se debe confirmar la transacción")]
        public async Task Commit()
        {
            //Arrange
            Noticia noticia = new Noticia() { Titulo = "Volcan", Contenido = "Peligro" };

            //Act
            await _Repository.AddAsync(noticia);
            await _UnitOfWork.CommitAsync();
            Noticia noticiaAdded =  await _Repository.GetAsync(noticia.Id);

            //Assert
            Assert.NotNull(noticiaAdded);
        }

        [Fact(DisplayName = "DADA una nueva Noticia que persiste CUANDO se realiza la reversión ENTONCES la transacción debe cancelarse Y la Noticia no debe mantenerse en el repositorio")]
        public async Task Rollback()
        {
            //Arrange
            Noticia noticia = new Noticia() { Titulo = "Volcan", Contenido = "Peligro" };

            //Act
            await _Repository.AddAsync(noticia);
            await _UnitOfWork.RollbackAsync();
            Noticia noticiaAdded =  await _Repository.GetAsync(noticia.Id);

            //Assert
            Assert.Null(noticiaAdded);
        }

        [Fact(DisplayName = "DADO que se modifica una Noticia en la memoria CUANDO no se realiza la actualización Y se realiza la confirmación ENTONCES entonces la Noticia no debe modificarse en el repositorio")]
        public async Task CommitWithoutChanges()
        {
            //Arrange
            Noticia noticia = _Repository.GetAll().First();
            string firstName = noticia.Titulo;
            noticia.Titulo = "Modified";

            //Act
            await _UnitOfWork.CommitAsync();
            Noticia noticiaUnchanged = await _Repository.GetAsync(noticia.Id);

            //Assert
            Assert.Equal(firstName, noticiaUnchanged.Titulo);
        }
    }
}
