namespace Infraestructure.Client
{
    public class ComentarioApi : IComentarioApi
    {
        private readonly HttpClient _httpClient;

        public ComentarioApi()
        {
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("https://localhost:7192/api/");
        }
    }
}