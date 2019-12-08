using ApiGateway.Exceptions;
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
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while adding food!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<bool> DeleteFood(int id)
        {
            var resp = await _httpClient.DeleteAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return true;
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while deleting food!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Food>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting food by id!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Food>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting all food!\n" +
                     $"Code {resp.StatusCode} with {content}.");

        }
        public async Task<bool> HealthCheck()
        {
            var resp = await _httpClient.GetAsync("status");

            return resp.IsSuccessStatusCode;
        }
    }
}
