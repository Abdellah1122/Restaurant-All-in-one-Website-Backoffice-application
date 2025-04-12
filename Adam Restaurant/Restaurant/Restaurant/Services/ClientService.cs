using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class ClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ClientService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
            _httpClient = httpClient;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<List<Client>>("api/Clients");
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<Client>($"api/Clients/{id}");
        }

        public async Task<bool> UpdateClientAsync(int id, Client client)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Clients/{id}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Clients", client);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Client>();
            }
            return null;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Clients/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
