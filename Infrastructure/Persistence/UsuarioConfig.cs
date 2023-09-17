using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence
{
    public class UsuarioConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.ToTable("Usuarios");
            entityBuilder.HasKey(r => r.UsuarioId);
        }
    }
}