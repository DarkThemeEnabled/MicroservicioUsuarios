//using System.Text;
//using Application.Exceptions;
//using Application.Interfaces;
//using Domain.DTO;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace Application.UserService
//{
//    public class UserComentarioService : IUserComentarioService
//    {
//        private readonly HttpClient _httpClient;

//        public UserComentarioService()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = new Uri("https://localhost:7231/api/");
//        }
//        public ComentarioDTO GetByID(int id)
//        {
//            var response = _httpClient.GetAsync($"Comentarios/{id}").Result;

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Error al obtener el comentario");
//            }

//            var content = response.Content.ReadAsStringAsync().Result;
//            return JsonConvert.DeserializeObject<ComentarioDTO>(content);
//        }

//        public string GetComentarioReceta(int id)
//        {
//            try
//            {
//                HttpResponseMessage response = _httpClient.GetAsync($"Receta/ById/{id}").Result;

//                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
//                {
//                    throw new ExceptionNotFound("No existe la receta con el id: ");
//                }
//                dynamic receta = response.Content.ReadAsAsync<dynamic>().Result;

//                return receta;
//            }
//            catch (ExceptionNotFound e)
//            {
//                throw new ExceptionNotFound(e.Message);
//            }
//        }
//        public void CreateComentario(ComentarioDTO comentarioData, string userToken)
//        {
//            var jsonContent = JsonConvert.SerializeObject(comentarioData);
//            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

//            // Usamos el token proporcionado para autenticar la solicitud
//            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {userToken}");

//            var response = _httpClient.PostAsync("Comentarios", content).Result;

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Error al crear el comentario");
//            }
//        }
//    }
//}