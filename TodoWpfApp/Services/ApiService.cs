using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TodoWpfApp.Models;

namespace TodoWpfApp.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService(string baseUrl = "http://localhost:5037")
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        // --- TODO ENDPOINTS ---
        public async Task<List<TodoItemDto>> GetTodosAsync() =>
            await _http.GetFromJsonAsync<List<TodoItemDto>>("/todos", _jsonOptions) ?? new();

        public async Task CreateTodoAsync(TodoSavePayload todo) =>
            await _http.PostAsJsonAsync("/todos", todo);

        public async Task UpdateTodoAsync(int id, TodoSavePayload todo) =>
            await _http.PutAsJsonAsync($"/todos/{id}", todo);

        public async Task DeleteTodoAsync(int id) =>
            await _http.DeleteAsync($"/todos/{id}");

        // --- MEMBER ENDPOINTS ---
        public async Task<List<MemberDto>> GetMembersAsync() =>
            await _http.GetFromJsonAsync<List<MemberDto>>("/members", _jsonOptions) ?? new();

        public async Task CreateMemberAsync(MemberDto member) =>
            await _http.PostAsJsonAsync("/members", member);

        public async Task DeleteMemberAsync(int id) =>
            await _http.DeleteAsync($"/members/{id}");

        // --- PLACE ENDPOINTS ---
        public async Task<List<PlaceDto>> GetPlacesAsync() =>
            await _http.GetFromJsonAsync<List<PlaceDto>>("/places", _jsonOptions) ?? new();

        public async Task CreatePlaceAsync(PlaceDto place) =>
            await _http.PostAsJsonAsync("/places", place);

        public async Task DeletePlaceAsync(int id) =>
            await _http.DeleteAsync($"/places/{id}");
    }
}
