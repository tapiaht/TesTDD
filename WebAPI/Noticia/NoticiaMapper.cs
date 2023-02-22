using AutoMapper;
using Entity = TesTDD.Domain.Core.Entity;

namespace TesTDD.WebAPI.Noticia
{
    public class NoticiaMapper : Profile
    {
        public NoticiaMapper()
        {
            CreateMap<Entity.Noticia, NoticiaModel>();
            CreateMap<NoticiaModel, Entity.Noticia>();
        }
    }
}
