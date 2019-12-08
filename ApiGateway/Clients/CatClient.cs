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
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while adding cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCats()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCats(int size, int pageSize)
        {
            var resp = await _httpClient.GetAsync($"?page={size}&?pageSize={pageSize}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<bool> DeleteCat(int id)
        {
            var resp = await _httpClient.DeleteAsync($"{id}");

            if (resp.IsSuccessStatusCode)
                return true;
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException("Bat request.");
            else
                throw new InternalException($"Error while delete cat!\n");
        }

        public async Task<Cat> GetCatByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Cat>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting cat by id!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }
        

        public async Task<IEnumerable<Cat>> DeleteCatsByOwnerIdAsync(int ownerId)
        {
            var resp = await _httpClient.DeleteAsync($"owner/{ownerId}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while deleting cats!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCatsByOwnerIdAsync(int ownerId)
        {
            var resp = await _httpClient.GetAsync($"owner/{ownerId}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting cats!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }
        public async Task<bool> HealthCheck()
        {
            var resp = await _httpClient.GetAsync("status");

            return resp.IsSuccessStatusCode;
        }
}
}
