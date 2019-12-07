using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

            return null;

        }

        public Task<Owner> DeleteOwner(Owner owner)
        {
            throw new NotImplementedException();
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);

            return null;
        }

        public async Task<IEnumerable<Owner>> GetOwners()
        {
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Owner>>(content);

            return null;
        }
    }
}
