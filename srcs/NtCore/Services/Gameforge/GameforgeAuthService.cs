using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NtCore.Extensions;
using NtCore.Serialization;
using NtCore.Services.Gameforge.Forms;

namespace NtCore.Services.Gameforge
{
    public class GameforgeAuthService : IGameforgeAuthService
    {
        private const string Url = "https://spark.gameforge.com/api/v1";
        private const string PlatformId = "dd4e22d6-00d1-44b9-8126-d8b40e0cd7c9";
        private const string UserAgent = "GameforgeClient/2.0.48";
        
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public GameforgeAuthService(ISerializer serializer)
        {
            _httpClient = new HttpClient();
            _serializer = serializer;
        }
        
        public async Task<GameforgeAccount> GetAccount(string username, string password, Language language)
        {
            string serialized = _serializer.Serialize(new AuthForm
            {
                GfLang = language.Name,
                Identity = username,
                Locale = language.Locale,
                Password = password,
                PlatformGameId = PlatformId
            });
            
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}/auth/thin/sessions"))
            {
                request.Headers.Add("User-Agent", UserAgent);
                request.Content = new StringContent(serialized, Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();
                
                string content = await response.Content.ReadAsStringAsync();

                return _serializer.Deserialize<GameforgeAccount>(content);
            }
        }

        public async Task<string> GetToken(GameforgeAccount account, Guid installationId = default)
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
                    PlatformAccountId = account.PlatformGameAccountId
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
}