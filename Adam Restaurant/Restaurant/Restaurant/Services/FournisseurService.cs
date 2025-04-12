using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Restaurant.Models.Classes;

public class FournisseurService
{
	private readonly HttpClient _httpClient;
	private readonly ILocalStorageService _localStorage;
	public FournisseurService(HttpClient httpClient,ILocalStorageService localStorage)
	{
		_localStorage = localStorage;
		_httpClient = httpClient;
	}

	// Get all fournisseurs
	public async Task<List<Fournisseur>> GetFournisseursAsync()
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		return await _httpClient.GetFromJsonAsync<List<Fournisseur>>("api/Fournisseurs");
	}

	// Get a single fournisseur by ID
	public async Task<Fournisseur> GetFournisseurByIdAsync(int id)
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		return await _httpClient.GetFromJsonAsync<Fournisseur>($"api/Fournisseurs/{id}");
	}

	// Create a new fournisseur
	public async Task<bool> CreateFournisseurAsync(Fournisseur fournisseur)
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		var response = await _httpClient.PostAsJsonAsync("api/Fournisseurs", fournisseur);
		return response.IsSuccessStatusCode;
	}

	// Update an existing fournisseur
	public async Task<bool> UpdateFournisseurAsync(int id, Fournisseur fournisseur)
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		var response = await _httpClient.PutAsJsonAsync($"api/Fournisseurs/{id}", fournisseur);
		return response.IsSuccessStatusCode;
	}

	// Delete a fournisseur
	public async Task<bool> DeleteFournisseurAsync(int id)
	{
		var token = await _localStorage.GetItemAsync<string>("authToken");

		if (!string.IsNullOrEmpty(token))
		{
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
		var response = await _httpClient.DeleteAsync($"api/Fournisseurs/{id}");
		return response.IsSuccessStatusCode;
	}
}

