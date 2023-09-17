namespace Application.Request
{
    public class EliminarRecetaRequest
    {
        public string UsuarioId { get; set; }
        public Guid RecetaId { get; set; }
    }
}