using Blazored.LocalStorage;
using Restaurant.Models.Classes;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Restaurant.Services
{
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;

		private readonly ILocalStorageService _localStorage;

		public EmployeeService(HttpClient httpClient , ILocalStorageService localStorage)
        {

            _httpClient = httpClient;
			_localStorage = localStorage;
        }

        // Get all employees
        public async Task<List<Employee>> GetEmployeesAsync()
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<List<Employee>>("api/Employees");
        }

        // Get a single employee by ID
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			return await _httpClient.GetFromJsonAsync<Employee>($"api/Employees/{id}");
        }

        // Create a new employee
        public async Task<bool> CreateEmployeeAsync(Employee employee)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PostAsJsonAsync("api/Employees", employee);
            return response.IsSuccessStatusCode;
        }

        // Update an existing employee
        public async Task<bool> UpdateEmployeeAsync(int id, Employee employee)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.PutAsJsonAsync($"api/Employees/{id}", employee);
            return response.IsSuccessStatusCode;
        }

        // Delete an employee
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
			var token = await _localStorage.GetItemAsync<string>("authToken");

			if (!string.IsNullOrEmpty(token))
			{
				_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			var response = await _httpClient.DeleteAsync($"api/Employees/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}