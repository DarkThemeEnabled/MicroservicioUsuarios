namespace Infraestructure.Client
{
    public class RecetaApi
    {
        private readonly HttpClient _httpClient;

        public RecetaApi()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7015/api/");
        }
    }
}