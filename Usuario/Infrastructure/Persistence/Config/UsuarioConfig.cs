using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Config
{
    public class RecetaConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> entityBuilder)
        {
            entityBuilder.ToTable("Usuario");

            // Configuración de clave primaria
            entityBuilder.HasKey(u => u.UsuarioId);

            // Un usuario tiene muchas recetas o ninguna, y una receta pertenece a un usuario obligatorio
            entityBuilder.HasMany(u => u.Recetas)
                          .WithOne(r => r.Usuario)
                          .HasForeignKey(r => r.UsuarioId)
                          .IsRequired();

            // Un usuario puede hacer muchos comentarios o ninguno, y un comentario pertenece a un usuario obligatorio
            entityBuilder.HasMany(u => u.Comentarios)
                          .WithOne(c => c.Usuario)
                          .HasForeignKey(c => c.UsuarioId)
                          .IsRequired();
        }
    }
}