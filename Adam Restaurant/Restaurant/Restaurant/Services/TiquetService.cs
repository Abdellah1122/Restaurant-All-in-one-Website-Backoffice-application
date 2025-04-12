using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class TiquetService 
    {
        private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public TiquetService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
			_localStorage = localStorage;
		}

        public async Task<List<Tiquet>> GetTiquetsAsync()
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<List<Tiquet>>("api/tiquets");
        }

        public async Task<Tiquet> GetTiquetAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Tiquet>($"api/tiquets/{id}");
        }

        public async Task<Tiquet> CreateTiquetAsync(Tiquet tiquet)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/tiquets", tiquet);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Tiquet>();
            }
            return null;
        }
    }

}
