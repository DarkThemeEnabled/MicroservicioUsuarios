using Domain.DTO;

namespace Application.Interfaces
{
    public interface IUserRecetaService
    {
        RecetaDTO GetByID(int id, string userToken);
        string GetRecetaName(int id, string userToken);
        RecetaDTO[] GetRecetas(string userToken);
    }
}
