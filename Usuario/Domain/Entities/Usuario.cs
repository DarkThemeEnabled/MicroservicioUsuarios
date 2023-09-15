namespace Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public int FotoPerfil { get; set; }

        // Identificadores de recetas y comentarios asociados con este usuario
        public required List<int> RecetaIds { get; set; }
        public required List<int> ComentarioIds { get; set; }
    }
}
