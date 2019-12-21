using ApiGateway.Exceptions;
using ApiGateway.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static ApiGateway.Settings.AuthService;

namespace ApiGateway.Clients
{
    public class FoodClient : IFoodClient
    {
        private readonly HttpClient _httpClient;
        private AuthToService _auth;
        public FoodClient(HttpClient httpClient, AuthToService auth)
        {
            _httpClient = httpClient;
            _auth = auth;
            
        }

        public async Task<Food> AddNewFood(Food food)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FoodAuth", _auth.FoodToken);
            var catJson = JsonConvert.SerializeObject(food);
            StringContent httpContent = new StringContent(catJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Food>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthFood();
                return await AddNewFood(food);
            }
            else
                throw new InternalException($"Error while adding food!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<bool> DeleteFood(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FoodAuth", _auth.FoodToken);
            var resp = await _httpClient.DeleteAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return true;
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthFood();
                return await DeleteFood(id);
            }
            else
                throw new InternalException($"Error while deleting food!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<Food> GetFoodByIdAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FoodAuth", _auth.FoodToken);
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Food>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthFood();
                return await GetFoodByIdAsync(id);
            }
            else
                throw new InternalException($"Error while getting food by id!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Food>> GetFoods()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FoodAuth", _auth.FoodToken);
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Food>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthFood();
                return await GetFoods();
            }
            else
                throw new InternalException($"Error while getting all food!\n" +
                     $"Code {resp.StatusCode} with {content}.");

        }

        private async Task<bool> AuthFood()
        {

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(_auth.FoodCred));

            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync("auth", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
            {
                TokenM token = JsonConvert.DeserializeObject<TokenM>(content);
                _auth.FoodToken = token.Token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("FoodAuth", _auth.FoodToken);
                return true;
            }
            else
                throw new InternalException($"Error during auth to Owner!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<bool> HealthCheck()
        {
            var resp = await _httpClient.GetAsync("status");

            return resp.IsSuccessStatusCode;
        }
    }
}
