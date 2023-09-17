namespace Domain.DTO
{
    public class ComentarioDTO
    {
        public int ComentarioId { get; set; }
        public int UsuarioId { get; set; }
        public int PromedioPuntajeId { get; set; }
        public string Comentario { get; set; }
        public int PuntajeReceta { get; set; }
    }
}
