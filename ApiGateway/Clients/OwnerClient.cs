using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ApiGateway.Exceptions;
using ApiGateway.Models;
using Newtonsoft.Json;
using static ApiGateway.Settings.AuthService;

namespace ApiGateway.Clients
{
    public class OwnerClient : IOwnerClient
    {

        private readonly HttpClient _httpClient;
        private readonly AuthToService _auth;

        public OwnerClient(HttpClient httpClient, AuthToService auth)
        {
            _httpClient = httpClient;
            _auth = auth;
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OwnerAuth", _auth.OwnerToken);
            var ownerJson = JsonConvert.SerializeObject(owner);
            StringContent httpContent = new StringContent(ownerJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthOwner();
                return await AddOwner(owner);
            }
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while adding owner!\n" +
                     $"Code {resp.StatusCode} with {content}.");

        }

        public async Task<Owner> DeleteOwner(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OwnerAuth", _auth.OwnerToken);
            var resp = await _httpClient.DeleteAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthOwner();
                return await DeleteOwner(id);
            }
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while deleting owner!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OwnerAuth", _auth.OwnerToken);
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Owner>(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthOwner();
                return await GetOwnerByIdAsync(id);
            }
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting owner by id!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Owner>> GetOwners()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OwnerAuth", _auth.OwnerToken);
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Owner>>(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthOwner();
                return await GetOwners();
            }
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else
                throw new InternalException($"Error while getting all owners!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        private async Task<bool> AuthOwner()
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(_auth.OwnerCred));

            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync("auth", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
            {
                TokenM token = JsonConvert.DeserializeObject<TokenM>(content);
                _auth.OwnerToken = token.Token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OwnerAuth", _auth.OwnerToken);
                return true;
            }
            else
                throw new InternalException($"Error during auth to Owner!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }


        public async Task<bool> GetTokenCorrectness(string token)
        {
            var resp = await _httpClient.GetAsync("status");

            return true;
        }
        public async Task<bool> HealthCheck()
        {

            var resp = await _httpClient.GetAsync("status");

            return resp.IsSuccessStatusCode;
        }
    }
}
