using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TodoWpf.App.Models;

namespace TodoWpf.App.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions? _jsonOptions;
        public ApiService(string baseUrl = "http://localhost:5107")
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
        public async Task<List<TodoItemDto>> GetTodosAsync() =>
            await _httpClient.GetFromJsonAsync<List<TodoItemDto>>("/todos", _jsonOptions) ?? new List<TodoItemDto>();

        public async Task CreateTodoAsync(TodoSavePload todo) =>
            await _httpClient.PostAsJsonAsync("/todos", todo);

        public async Task UpdateTodoAsync(int id, TodoSavePload todo) =>
            await _httpClient.PutAsJsonAsync($"/todos/{id}", todo);

        public async Task DeleteTodoAsync(int id) =>
            await _httpClient.DeleteAsync($"/todos/{id}");

        public async Task<List<MemberDto>> GetMembersAsync() =>
            await _httpClient.GetFromJsonAsync<List<MemberDto>>("/members", _jsonOptions) ?? new();

        public async Task CreateMemberAsync(MemberDto member) =>
            await _httpClient.PostAsJsonAsync("/members", member);

        public async Task<List<PlaceDto>> GetPlacesAsync() =>
            await _httpClient.GetFromJsonAsync<List<PlaceDto>>("/places", _jsonOptions) ?? new();

        public async Task CreatePlaceAsync(PlaceDto place) =>
            await _httpClient.PostAsJsonAsync("/places", place);

    }
}
