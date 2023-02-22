using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesTDD.Domain.Core.Entity;

namespace TesTDD.Infra.Repository.Configurations
{
    public class NoticiaTypeConfiguration : IEntityTypeConfiguration<Noticia>
    {
        public void Configure(EntityTypeBuilder<Noticia> builder)
        {
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.Titulo).HasMaxLength(50);
            builder.Property(o => o.Contenido).HasMaxLength(50);
        }
    }
}