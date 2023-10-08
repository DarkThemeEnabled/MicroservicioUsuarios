using Domain.DTO;
using System.Net.Http.Json;

namespace Infrastructure.Client
{
    public class RecetaApi
    {
        private readonly HttpClient _httpClient;

        public RecetaApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener una receta por ID
        public async Task<RecetaDTO> ObtenerRecetaPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"recetas/{id}");
            response.EnsureSuccessStatusCode();
            var receta = await response.Content.ReadAsAsync<RecetaDTO>();
            return receta;
        }

        // Obtener todas las recetas
        public async Task<IEnumerable<RecetaDTO>> ObtenerRecetasAsync()
        {
            var response = await _httpClient.GetAsync("recetas");
            response.EnsureSuccessStatusCode();
            var recetas = await response.Content.ReadAsAsync<IEnumerable<RecetaDTO>>();
            return recetas;
        }

        // Crear una nueva receta
        public async Task<RecetaDTO> CrearRecetaAsync(RecetaDTO nuevaReceta)
        {
            var response = await _httpClient.PostAsJsonAsync("recetas", nuevaReceta);
            response.EnsureSuccessStatusCode();
            var recetaCreada = await response.Content.ReadAsAsync<RecetaDTO>();
            return recetaCreada;
        }

        // Actualizar una receta
        public async Task<RecetaDTO> ActualizarRecetaAsync(int id, RecetaDTO recetaActualizada)
        {
            var response = await _httpClient.PutAsJsonAsync($"recetas/{id}", recetaActualizada);
            response.EnsureSuccessStatusCode();
            var recetaActualizadaResponse = await response.Content.ReadAsAsync<RecetaDTO>();
            return recetaActualizadaResponse;
        }

        // Eliminar una receta
        public async Task EliminarRecetaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"recetas/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}