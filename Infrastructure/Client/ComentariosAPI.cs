using Domain.DTO;
using System.Net.Http.Json;

namespace Infrastructure.Client
{
    public class ComentarioApi
    {
        private readonly HttpClient _httpClient;

        public ComentarioApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener comentarios por ID de receta
        public async Task<IEnumerable<ComentarioDTO>> ObtenerComentariosPorRecetaIdAsync(int recetaId)
        {
            var response = await _httpClient.GetAsync($"comentarios/{recetaId}");
            response.EnsureSuccessStatusCode();
            var comentarios = await response.Content.ReadAsAsync<IEnumerable<ComentarioDTO>>();
            return comentarios;
        }

        // Obtener un comentario por ID
        public async Task<ComentarioDTO> ObtenerComentarioPorIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"comentarios/detalle/{id}");
            response.EnsureSuccessStatusCode();
            var comentario = await response.Content.ReadAsAsync<ComentarioDTO>();
            return comentario;
        }

        // Crear un nuevo comentario
        public async Task<ComentarioDTO> CrearComentarioAsync(ComentarioDTO nuevoComentario)
        {
            var response = await _httpClient.PostAsJsonAsync("comentarios", nuevoComentario);
            response.EnsureSuccessStatusCode();
            var comentarioCreado = await response.Content.ReadAsAsync<ComentarioDTO>();
            return comentarioCreado;
        }

        // Actualizar un comentario
        public async Task<ComentarioDTO> ActualizarComentarioAsync(int id, ComentarioDTO comentarioActualizado)
        {
            var response = await _httpClient.PutAsJsonAsync($"comentarios/{id}", comentarioActualizado);
            response.EnsureSuccessStatusCode();
            var comentarioActualizadoResponse = await response.Content.ReadAsAsync<ComentarioDTO>();
            return comentarioActualizadoResponse;
        }

        // Eliminar un comentario
        public async Task EliminarComentarioAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"comentarios/{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}