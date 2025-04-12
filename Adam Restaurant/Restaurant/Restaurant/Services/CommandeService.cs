using Restaurant.Models.Classes;
using System.Net.Http.Json;

namespace Restaurant.Services
{
	public class CommandeService
	{
		private readonly HttpClient _httpClient;

		public CommandeService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<List<Commande>> GetCommandesAsync()
		{
			return await _httpClient.GetFromJsonAsync<List<Commande>>("api/commandes");
		}

		public async Task<Commande> GetCommandeAsync(int id)
		{
			return await _httpClient.GetFromJsonAsync<Commande>($"api/commandes/{id}");
		}

		public async Task<Commande> CreateCommandeAsync(Commande commande)
		{
			var response = await _httpClient.PostAsJsonAsync("api/commandes", commande);
			response.EnsureSuccessStatusCode();
			return await response.Content.ReadFromJsonAsync<Commande>();
		}

		public async Task UpdateCommandeAsync(int id, Commande commande)
		{
			var response = await _httpClient.PutAsJsonAsync($"api/commandes/{id}", commande);
			response.EnsureSuccessStatusCode();
		}

		public async Task DeleteCommandeAsync(int id)
		{
			var response = await _httpClient.DeleteAsync($"api/commandes/{id}");
			response.EnsureSuccessStatusCode();
		}
	}
}
