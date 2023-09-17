namespace Application.Request
{
    public class ComentarRecetaRequest
    {
        public string UsuarioId { get; set; }
        public int RecetaId { get; set; }
        public string Comentario { get; set; }
    }
}
