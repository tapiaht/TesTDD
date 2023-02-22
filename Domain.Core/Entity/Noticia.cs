using System;

namespace TesTDD.Domain.Core.Entity
{
    public class Noticia : BaseEntity<Guid>
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
    }
}