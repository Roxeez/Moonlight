using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NtCore.Extensions;

namespace NtCore.Clients.Remote
{
    public class GameforgeAuth : IGameforgeAuth
    {
        private const string Url = "https://spark.gameforge.com/api/v1";
        private const string PlatformId = "dd4e22d6-00d1-44b9-8126-d8b40e0cd7c9";
        private const string UserAgent = "GameforgeClient/2.0.48";
        
        private readonly HttpClient _httpClient;

        public GameforgeAuth() => _httpClient = new HttpClient();

        public async Task<Account> GetAccount(string username, string password, Language language)
        {
            string json = JsonConvert.SerializeObject(new AuthForm
            {
                GfLang = language.Name,
                Identity = username,
                Locale = language.Locale,
                Password = password,
                PlatformGameId = PlatformId
            });

            HttpResponseMessage response = await _httpClient.PostAsync($"{Url}/auth/thin/sessions", new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Account>(content);
        }
        
        public async Task<string> GetSessionToken(Account account, Guid installationId = default)
        {
            if (installationId == default)
            {
                installationId = Guid.NewGuid();
            }
            
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}/auth/thin/codes"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", account.Token);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());
                request.Headers.Add("User-Agent", UserAgent);

                var form = new SessionForm
                {
                    PlatformAccountId = account.PlatformAccountId
                };
                
                request.Content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                
                string content = await response.Content.ReadAsStringAsync();
                var jsonContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                
                return jsonContent.GetValueOrDefault("code");
            }
        }
    }

    public class SessionForm
    {
        [JsonProperty(PropertyName = "platformGameAccountId")]
        public string PlatformAccountId { get; set; }
    }
    
    public class AuthForm
    {
        [JsonProperty(PropertyName = "gfLang")]
        public string GfLang { get; set; }
        
        [JsonProperty(PropertyName = "identity")]
        public string Identity { get; set; }
        
        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }
        
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "platformGameId")]
        public string PlatformGameId { get; set; }
    }
}