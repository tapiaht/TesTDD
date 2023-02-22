using System;

namespace TesTDD.WebAPI.Noticia
{
    public class NoticiaModel
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}