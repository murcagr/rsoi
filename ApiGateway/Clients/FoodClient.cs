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
    public class FoodClient : IFoodClient
    {
        private readonly HttpClient _httpClient;

        public FoodClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Food> AddNewFood(Food food)
        {
            var catJson = JsonConvert.SerializeObject(food);
            StringContent httpContent = new StringContent(catJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Food>(content);

            return null;
        }

        public async Task<bool> DeleteFood(int id)
        {
            var resp = await _httpClient.DeleteAsync($"{id}");

            return true;
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();
            var cats = JsonConvert.DeserializeObject<Food>(content);

            return cats;
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Food>>(content);

            return null;
        }
    }
}
