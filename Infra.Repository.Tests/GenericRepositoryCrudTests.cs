using Microsoft.Extensions.DependencyInjection;
using TesTDD.Domain.Core.Entity;
using TesTDD.Domain.Core.Interfaces.Repository;
using TesTDD.Infra.Repository.Tests.Fixtures;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TesTDD.Infra.Repository.Tests
{
    public class GenericRepositoryCrudTests : IClassFixture<DependencyInjectionFixture>,
                                              IClassFixture<NoticiaFixture>
    {
        private readonly GenericRepositoryCrud<Guid, Noticia> _Repository;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly RepositoryContext _Context;
        private readonly NoticiaFixture _NoticiaFixture;

        public GenericRepositoryCrudTests(DependencyInjectionFixture dependencyInjectionFixture, NoticiaFixture noticiaFixture)
        {
            IServiceProvider serviceProvider = dependencyInjectionFixture._ServiceProvider;

            _Repository = serviceProvider.GetService<GenericRepositoryCrud<Guid, Noticia>>();
            _UnitOfWork = serviceProvider.GetService<IUnitOfWork>();
            _Context = serviceProvider.GetService<RepositoryContext>();

            _NoticiaFixture = noticiaFixture;
        }

        [Fact(DisplayName = "DADA una Noticia CUANDO se persiste en el repositorio ENTONCES se debe generar una Id automáticamente")]
        public async Task AddNoticia()
        {
            //Arrange
            Noticia noticia = _NoticiaFixture.CreateValid();
            
            //Act
            await _Repository.AddAsync(noticia);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(noticia);
            Assert.NotEqual(default, noticia.Id);
        }
        
        [Fact(DisplayName = "DADA una Noticia existente CUANDO eliminarla del repositorio ENTONCES la Noticia debe ser devuelta")]
        public async Task DeleteNoticiaExists()
        {
            //Arrange
            Guid id = _Context.Set<Noticia>().Select(o => o.Id).First();

            //Act
            Noticia noticia = await _Repository.DeleteAsync(id);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(noticia);
            Assert.Equal(id, noticia.Id);
        }

        [Fact(DisplayName = "DADA una Noticia inexistente CUANDO la elimina del repositorio ENTONCES devuelve NULL")]
        public async Task DeleteNoticiaNotExists()
        {
            //Arrange
            Guid id = Guid.Parse("c87c6eb2-715f-4553-be17-16bfbdd21725");

            //Act
            Noticia noticia = await _Repository.DeleteAsync(id);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.Null(noticia);
        }

        [Fact(DisplayName = "DADA una Noticia existente CUANDO realizar una consulta por Id ENTONCES se debe devolver la Noticia")]
        public async Task GetNoticiaExists()
        {
            //Arrange
            Guid id = _Context.Set<Noticia>().Select(o => o.Id).First();

            //Act
            Noticia noticia = await _Repository.GetAsync(id);

            //Assert
            Assert.NotNull(noticia);
            Assert.Equal(id, noticia.Id);
        }

        [Fact(DisplayName = "DADA una Noticia inexistente AL realizar una consulta por Id ENTONCES devuelve NULL")]
        public async Task GetNoticiaNotExists()
        {
            //Arrange
            Guid id = Guid.Parse("c87c6eb2-715f-4553-be17-16bfbdd21725");

            //Act
            Noticia noticia = await _Repository.GetAsync(id);

            //Assert
            Assert.Null(noticia);
        }

        [Fact(DisplayName = "DADO que tenemos al menos una Noticia en el repositorio CUANDO realizar un listado ENTONCES todos los registros deben ser devueltos")]
        public void GetAllNoticiaExists()
        {
            //Arrange

            //Act
            var noticias = _Repository.GetAll();

            //Assert
            Assert.NotNull(noticias);
            Assert.NotEmpty(noticias);
        }

        [Fact(DisplayName = "DADA una Noticia existente CUANDO actualice el Título ENTONCES la Noticia debe ser devuelta")]
        public async Task UpdateNoticiaExists()
        {
            //Arrange
            Guid id = _Context.Set<Noticia>().Select(o => o.Id).First();

            //Act
            Noticia noticia = await _Repository.GetAsync(id);
            noticia.Titulo = $"{noticia.Titulo} (MODIFICADO)";

            Noticia noticiaUpdated = await _Repository.UpdateAsync(noticia);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.NotNull(noticiaUpdated);
            Assert.Equal(noticia.Titulo, noticiaUpdated.Titulo);
        }

        [Fact(DisplayName = "DADA una Noticia inexistente CUANDO la actualizas en el repositorio ENTONCES devuelve NULL")]
        public async Task UpdateNoticiaNotExists()
        {
            //Arrange
            Noticia noticia = new Noticia { Id = Guid.NewGuid() };

            //Act
            Noticia noticiaUpdated = await _Repository.UpdateAsync(noticia);
            await _UnitOfWork.CommitAsync();

            //Assert
            Assert.Null(noticiaUpdated);
        }
    }
}
