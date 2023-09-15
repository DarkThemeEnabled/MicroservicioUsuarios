namespace Infraestructure.Client
{
    public class RecetaApi : IRecetaApi
    {
        private readonly HttpClient _httpClient;

        public RecetaApi()
        {
            _httpClient = new HttpClient();
            //_httpClient.BaseAddress = new Uri("https://localhost:7192/api/");
        }
    }
}