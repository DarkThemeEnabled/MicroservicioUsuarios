namespace Domain.DTO
{
    public class RecetaDTO
    {
        public Guid RecetaId { get; set; }
        public int CategoriaRecetaId { get; set; }
        public int DificultadId { get; set; }
        public Guid UsuarioId { get; set; }
        public required string Titulo { get; set; }
        public required string FotoReceta { get; set; }
        public required string Video { get; set; }
        public TimeSpan TiempoPreparacion { get; set; }
    }
}
