using System.Net.Http.Json;
using SocialMediaApp.Models;

namespace SocialMediaApp.Services
{
    public class PostService
    {
        private readonly HttpClient _httpClient;
        public PostService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<Post>> GetAllPosts()
        {
            return await _httpClient.GetFromJsonAsync<List<Post>>("http://localhost:5002/api/posts");
        }

        public async Task<bool> CreatePost(Post post)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5002/api/posts", post);
            return response.IsSuccessStatusCode;
        }
    }
}
