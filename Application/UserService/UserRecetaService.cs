//using Application.Exceptions;
//using Application.Interfaces;
//using Domain.DTO;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Net.Http.Headers;

//namespace Application.UserService
//{
//    public class UserRecetaService: IUserRecetaService
//    {
//        private readonly HttpClient _httpClient;

//        public UserRecetaService()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = new Uri("https://localhost:7015/api/");
//        }

//        public RecetaDTO GetByID(int id, string userToken)
//        {
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
//            var response = _httpClient.GetAsync($"Receta/{id}").Result;

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Error al obtener la receta");
//            }

//            var content = response.Content.ReadAsStringAsync().Result;
//            return JsonConvert.DeserializeObject<RecetaDTO>(content);
//        }

//        public string GetRecetaName(int id, string userToken)
//        {
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
//            HttpResponseMessage response = _httpClient.GetAsync($"Receta/ById/{id}").Result;
//            string jsonContent = response.Content.ReadAsStringAsync().Result;
//            dynamic dynamicData = JObject.Parse(jsonContent);
//            return ($"{dynamicData.name}");
//        }

//        public RecetaDTO[] GetRecetas(string userToken)
//        {
//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userToken);
//            var response = _httpClient.GetAsync("Receta").Result;

//            if (!response.IsSuccessStatusCode)
//            {
//                throw new Exception("Error al obtener las recetas");
//            }

//            var content = response.Content.ReadAsStringAsync().Result;
//            return JsonConvert.DeserializeObject<RecetaDTO[]>(content);
//        }
//    }
//}