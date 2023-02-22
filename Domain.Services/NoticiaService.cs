using TesTDD.Domain.Core.Entity;
using TesTDD.Domain.Core.Interfaces.Repository;
using TesTDD.Domain.Core.Interfaces.Service;
using System;

namespace TesTDD.Domain.Services
{
    public class NoticiaService : GenericServiceCrud<Guid, Noticia>, INoticiaService
    {
        public NoticiaService(IRepositoryCrud<Guid, Noticia> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }
    }
}
