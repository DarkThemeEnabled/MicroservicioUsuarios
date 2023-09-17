using Domain.DTO;

namespace Application.Interfaces
{
    public interface IRecetaApi
    {
        Task<IEnumerable<RecetaDTO>> GetRecetas();
        Task<RecetaDTO> GetRecetaById(Guid id);
        Task<bool> CreateReceta(RecetaDTO receta);
        Task<bool> UpdateReceta(RecetaDTO receta);
        Task<bool> DeleteReceta(Guid id);
    }
}
