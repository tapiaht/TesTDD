using AutoMapper;
using TesTDD.Domain.Core.Interfaces.Service;
using TesTDD.WebAPI.Controllers;
using System;
using Entity = TesTDD.Domain.Core.Entity;

namespace TesTDD.WebAPI.Noticia
{
    public class NoticiaController : GenericControllerCrud<Guid, Entity.Noticia, NoticiaModel>
    {
        public NoticiaController(IServiceCrud<Guid, Entity.Noticia> service, IMapper mapper) : base(service, mapper) { }
    }
}
