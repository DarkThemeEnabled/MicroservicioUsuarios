using Application.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Application.UseServices
{
    public class ReservaService : IRecetaService
    {
        private readonly HttpClient _httpClient;

        public ReservaService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7015/api/");
        }

        public dynamic ModificarReceta(Guid recetaId, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = _httpClient.PostAsync($"Receta/{recetaId}").Result;


        }

        public dynamic ObtenerReceta(Guid recetaId, string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = _httpClient.GetAsync($"Receta/{recetaId}").Result;

            if (response.IsSuccessStatusCode)
            {
                dynamic receta = response.Content.ReadAsAsync<dynamic>().Result;
                return receta;
            }
            else
            {
                throw new Exception($"Error al obtener la receta. Código de respuesta: {response.StatusCode}");
            }
        }

        
    }
}