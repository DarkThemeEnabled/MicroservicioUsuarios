using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Config
{
    public class UsuarioContext : DbContext
    {
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RecetaConfig());
        }
    }
}
