using Domain.Entities;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class UsuarioContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfig());
            //modelBuilder.ApplyConfiguration(new UsuarioData());
        }
    }
}