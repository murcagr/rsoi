using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.Exceptions;
using ApiGateway.Models;
using Newtonsoft.Json;

namespace ApiGateway.Clients
{
    public class OwnerClient : IOwnerClient
    {

        private readonly HttpClient _httpClient;

        public OwnerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            var ownerJson = JsonConvert.SerializeObject(owner);
            StringContent httpContent = new StringContent(ownerJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while adding owner!\n" +
                     $"Code {resp.StatusCode} with {content}.");

        }

        public async Task<Owner> DeleteOwner(int id)
        {
            var resp = await _httpClient.DeleteAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while deleting owner!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting owner by id!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Owner>> GetOwners()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Owner>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting all owners!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }
        public async Task<bool> HealthCheck()
        {
            var resp = await _httpClient.GetAsync("status");

            return resp.IsSuccessStatusCode;
        }
    }
}
