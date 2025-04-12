using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class BookingService
    {
        private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;

		public BookingService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
			_localStorage = localStorage;
		}


        // Get all bookings
        public async Task<List<Booking>> GetBookingsAsync()
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<List<Booking>>("api/Bookings");
        }

        // Get a booking by ID
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<Booking>($"api/Bookings/{id}");
        }

        // Update an existing booking
        public async Task<bool> UpdateBookingAsync(int id, Booking booking)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Bookings/{id}", booking);
            return response.IsSuccessStatusCode;
        }

        // Create a new booking
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Bookings", booking);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Booking>();
            }
            return null;
        }

        // Delete a booking
        public async Task<bool> DeleteBookingAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Bookings/{id}");
            return response.IsSuccessStatusCode;
        }
        //
        // Confirmer une réservation
        public async Task<bool> SetReservationConfirmedAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/Bookings/{id}/Status/Confirmed", null);
            return response.IsSuccessStatusCode;
        }

        // Rejeter une réservation
        public async Task<bool> SetReservationRejectedAsync(int id)
        {
            var response = await _httpClient.PutAsync($"api/Bookings/{id}/Status/Rejected", null);
            return response.IsSuccessStatusCode;
        }
    }
}
