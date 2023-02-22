using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesTDD.WebAPI.Noticia
{
    public class NoticiaValidator : AbstractValidator<NoticiaModel>
    {
        public NoticiaValidator()
        {
            RuleFor(o => o.Titulo).NotEmpty();
            RuleFor(o => o.Contenido).NotEmpty();
        }
    }
}
