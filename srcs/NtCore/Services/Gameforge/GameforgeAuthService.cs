using System;
using System.Collections.Generic;
using System.Net;
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
        private const string URL = "https://spark.gameforge.com/api/v1";
        private const string PLATFORM_GAME_ID = "dd4e22d6-00d1-44b9-8126-d8b40e0cd7c9";
        private const string USER_AGENT = "GameforgeClient/2.0.48";

        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public GameforgeAuthService(ISerializer serializer)
        {
            _httpClient = new HttpClient();
            _serializer = serializer;
        }

        public async Task<GameforgeAccount> Connect(string username, string password, Language language)
        {
            string serialized = _serializer.Serialize(new AuthForm
            {
                GfLang = language.Name,
                Identity = username,
                Locale = language.Locale,
                Password = password,
                PlatformGameId = PLATFORM_GAME_ID
            });

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/auth/thin/sessions"))
            {
                request.Content = new StringContent(serialized, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var account = _serializer.Deserialize<GameforgeAccount>(content);

                return account;
            }
        }

        public async Task<string> GetToken(GameforgeAccount account, Guid installationId = default)
        {
            if (installationId == default)
            {
                installationId = Guid.NewGuid();
            }

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/auth/thin/codes"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", account.Token);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());
                request.Headers.Add("User-Agent", USER_AGENT);

                string json = _serializer.Serialize(new SessionForm
                {
                    PlatformGameAccountId = account.PlatformGameAccountId
                });

                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                string content = await response.Content.ReadAsStringAsync();
                var jsonContent = _serializer.Deserialize<Dictionary<string, string>>(content);

                string token = jsonContent.GetValueOrDefault("code");
                
                return token == null ? string.Empty : token.ToHex();
            }
        }
    }
}