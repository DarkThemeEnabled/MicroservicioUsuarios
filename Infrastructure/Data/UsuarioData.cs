using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class UsuarioData : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasData(
            new Usuario { UsuarioId = Guid.NewGuid(), Nombre = "Juditha", Apellido = "Magarrell", Email = "jmagarrell0@quantcast.com", FotoPerfil = "https://robohash.org/quiatemporeplaceat.jpg" },
            new Usuario { UsuarioId = Guid.NewGuid(), Nombre = "Tansy", Apellido = "O'Duggan", Email = "toduggan1@google.de", FotoPerfil = "https://robohash.org/aspernaturremtempore.jpg" },
            new Usuario { UsuarioId = Guid.NewGuid(), Nombre = "Dareen", Apellido = "McMorran", Email = "dmcmorran2@va.gov", FotoPerfil = "https://robohash.org/quasdoloribussit.jpg" },
            new Usuario { UsuarioId = Guid.NewGuid(), Nombre = "Towney", Apellido = "Pactat", Email = "tpactat3@hexun.com", FotoPerfil = "https://robohash.org/fugiteased.jpg" },
            new Usuario { UsuarioId = Guid.NewGuid(), Nombre = "Ciel", Apellido = "Kach", Email = "ckach4@gmail.com", FotoPerfil = "https://robohash.org/adquaeratrepellendus.jpg" }
            );
        }
    }
}