using Moq;
using TesTDD.Domain.Core.Entity;
using TesTDD.Domain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TesTDD.Domain.Services.Tests
{
    public class NoticiaServiceTests
    {
        [Fact(DisplayName = "DADA una nueva Noticia válida CUANDO intentamos persistirla ENTONCES debe persistir en el repositorio")]
        public async Task AddNoticia()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);

            Noticia noticia = new Noticia();

            //Act
            await _Service.AddAsync(noticia);

            //Assert
            _Repository.Verify(o => o.AddAsync(noticia));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADA una Noticia existente CUANDO intentamos eliminarla por Id ENTONCES debe eliminarse del repositorio")]
        public async Task DeleteNoticia()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();
            _Repository.Setup(o => o.DeleteAsync(It.IsAny<Guid>())).Returns<Guid>(id => Task.FromResult(new Noticia()));
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);
            Guid id = Guid.NewGuid();

            //Act
            await _Service.DeleteAsync(id);

            //Assert
            _Repository.Verify(o => o.DeleteAsync(id));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADA una Noticia inexistente CUANDO intentamos eliminarla por Id ENTONCES no debe eliminarse del repositorio")]
        public async Task DeleteNoticiaNotExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);
            Guid id = Guid.NewGuid();

            //Act
            await _Service.DeleteAsync(id);

            //Assert
            _Repository.Verify(o => o.DeleteAsync(id));
            _UnitOfWork.Verify(o => o.CommitAsync(default), Times.Never);
        }

        [Fact(DisplayName = "DADA una Noticia existente CUANDO intentamos realizar una actualización ENTONCES debe actualizarse desde el repositorio")]
        public async Task UpdateNoticia()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();
            _Repository.Setup(o => o.UpdateAsync(It.IsAny<Noticia>())).Returns<Noticia>(noticia => Task.FromResult(noticia));
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);
            Noticia noticia = new Noticia();

            //Act
            await _Service.UpdateAsync(noticia);

            //Assert
            _Repository.Verify(o => o.UpdateAsync(noticia));
            _UnitOfWork.Verify(o => o.CommitAsync(default));
        }

        [Fact(DisplayName = "DADA una Noticia inexistente CUANDO intentamos realizar una actualización ENTONCES no debería actualizarse desde el repositorio")]
        public async Task UpdateNoticiaNotExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();
            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();
            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);
            Noticia noticia = new Noticia();

            //Act
            await _Service.UpdateAsync(noticia);

            //Assert
            _Repository.Verify(o => o.UpdateAsync(noticia));
            _UnitOfWork.Verify(o => o.CommitAsync(default), Times.Never);
        }

        [Fact(DisplayName = "DADA una Noticia existente CUANDO intentamos realizar una consulta por Id ENTONCES debe ser devuelta")]
        public async Task GetNoticia()
        {
            //Arrange
            Guid id = Guid.NewGuid();

            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();

            _Repository
                .Setup(o => o.GetAsync(It.IsAny<Guid>()))
                .Returns<Guid>(o => Task.FromResult(new Noticia() { Id = o }));

            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();

            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);

            //Act
            Noticia noticia = await _Service.GetAsync(id);

            //Assert
            _Repository.Verify(o => o.GetAsync(id));
            Assert.Equal(id, noticia.Id);
        }

        [Fact(DisplayName = "DADO que tenemos al menos una Noticia almacenada en el repositorio CUANDO intentamos realizar un listado ENTONCES todo debe ser devuelto")]
        public void GetAllNoticiaExists()
        {
            //Arrange
            Mock<IRepositoryCrud<Guid, Noticia>> _Repository = new Mock<IRepositoryCrud<Guid, Noticia>>();

            _Repository.Setup(o => o.GetAll()).Returns(new List<Noticia>() { new Noticia(), new Noticia(), new Noticia() });

            Mock<IUnitOfWork> _UnitOfWork = new Mock<IUnitOfWork>();

            NoticiaService _Service = new NoticiaService(_Repository.Object, _UnitOfWork.Object);

            //Act
            IEnumerable<Noticia> noticias = _Service.GetAll();

            //Assert
            _Repository.Verify(o => o.GetAll());
            Assert.NotNull(noticias);
            Assert.NotEmpty(noticias);
            Assert.Equal(3, noticias.Count());
        }
    }
}