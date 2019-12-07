using ApiGateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Clients
{
    public class CatClient : ICatClient
    {
        private readonly HttpClient _httpClient;

        public CatClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Cat> AddNewCat(Cat cat)
        {
            var catJson = JsonConvert.SerializeObject(cat);
            StringContent httpContent = new StringContent(catJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Cat>(content);

            return null;
        }

        public async Task<IEnumerable<Cat>> GetCats()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();
            var cats = JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);

            return cats;
        }

        public async Task<bool> DeleteCat(int id)
        {
            var resp = await _httpClient.DeleteAsync($"{id}");

            return true;
        }

        public async Task<Cat> GetCatByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();
            var cats = JsonConvert.DeserializeObject<Cat>(content);

            return cats;
        }

        public async Task<IEnumerable<Cat>> GetCatsByOwnerIdAsync(int ownerId)
        {
            var resp = await _httpClient.GetAsync($"owner/{ownerId}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);

            return null;
        }
    }
}
