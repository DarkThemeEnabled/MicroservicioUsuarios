namespace Application.Interfaces
{
    public interface IRecetaService
    {
        dynamic ObtenerReceta(Guid recetaId, string token);
        dynamic ModificarReceta(Guid recetaId, string token);
    }
}
