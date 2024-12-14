using Movie.DTOs;

namespace Movie.Service
{

    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(configuration["Tmdb:BaseUrl"]);
            _apiKey = configuration["Tmdb:ApiKey"];
        }

        public async Task<IEnumerable<FilmDto>> GetPopularFilmsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"movie/popular?api_key={_apiKey}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<TmdbApiResponse>();
                return result.Results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPopularFilmsAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<FilmDetailsDto> GetFilmDetailsAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"movie/{id}?api_key={_apiKey}");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<FilmDetailsDto>();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error in GetFilmDetailsAsync: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<FilmDto>> SearchFilmsAsync(string query)
        {
            try
            {
                var response = await _httpClient.GetAsync($"search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<TmdbApiResponse>();
                return result.Results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchFilmsAsync: {ex.Message}");
                throw;
            }
        }
    }



}