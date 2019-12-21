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
    public class CatClient : ICatClient
    {
        private readonly HttpClient _httpClient;
        private AuthToService _auth;

        public CatClient(HttpClient httpClient, AuthToService auth)
        {
            _httpClient = httpClient;
            _auth = auth;
        }

        public async Task<Cat> AddNewCat(Cat cat)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var catJson = JsonConvert.SerializeObject(cat);
            StringContent httpContent = new StringContent(catJson, Encoding.UTF8, "application/json");
            var resp = await _httpClient.PostAsync("", httpContent);
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Cat>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await AddNewCat(cat);
            }
            else
                throw new InternalException($"Error while adding cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCats()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.GetAsync("");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await GetCats();
            }
            else
                throw new InternalException($"Error while getting cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCats(int size, int pageSize)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.GetAsync($"?page={size}&?pageSize={pageSize}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await GetCats(size, pageSize);
            }
            else
                throw new InternalException($"Error while getting cats!\n" +
                     $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<bool> DeleteCat(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.DeleteAsync($"{id}");

            if (resp.IsSuccessStatusCode)
                return true;
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException("Bat request.");
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await DeleteCat(id);
            }
            else
                throw new InternalException($"Error while delete cat!\n");
        }

        public async Task<Cat> GetCatByIdAsync(int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.GetAsync($"{id}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Cat>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await GetCatByIdAsync(id);
            }
            else
                throw new InternalException($"Error while getting cat by id!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }
        

        public async Task<IEnumerable<Cat>> DeleteCatsByOwnerIdAsync(int ownerId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.DeleteAsync($"owner/{ownerId}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await DeleteCatsByOwnerIdAsync(ownerId);
            }
            else
                throw new InternalException($"Error while deleting cats!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }

        public async Task<IEnumerable<Cat>> GetCatsByOwnerIdAsync(int ownerId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
            var resp = await _httpClient.GetAsync($"owner/{ownerId}");
            string content = await resp.Content.ReadAsStringAsync();

            if (resp.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<IEnumerable<Cat>>(content);
            else if (resp.StatusCode == HttpStatusCode.BadRequest)
                throw new RequestException(content);
            else if (resp.StatusCode == HttpStatusCode.Unauthorized)
            {
                await AuthCat();
                return await GetCatsByOwnerIdAsync(ownerId);
            }
            else
                throw new InternalException($"Error while getting cats!\n" +
                    $"Code {resp.StatusCode} with {content}.");
        }

        private async Task<bool> AuthCat()
        {

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(_auth.CatCred));

            StringContent httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var resp = await _httpClient.PostAsync("auth", httpContent);
            string content = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
            {
                TokenM token = JsonConvert.DeserializeObject<TokenM>(content);
                _auth.CatToken = token.Token;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("CatAuth", _auth.CatToken);
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
