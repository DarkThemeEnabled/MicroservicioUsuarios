namespace Application.Request
{
    public class ActualizarRecetaRequest
    {
        public string UsuarioId { get; set; }
        public Guid RecetaId { get; set; }
    }
}