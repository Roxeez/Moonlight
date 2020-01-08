using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NtCore.Core;
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
        private const string MEDIA_TYPE = "application/json";

        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;

        public GameforgeAuthService(ISerializer serializer)
        {
            _httpClient = new HttpClient();
            _serializer = serializer;
        }

        public async Task<Optional<string>> GetAuthToken(string username, string password, Language language)
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
                request.Content = new StringContent(serialized, Encoding.UTF8, MEDIA_TYPE);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return Optional.Empty<string>();
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonContent = _serializer.Deserialize<Dictionary<string, string>>(content);

                string token = jsonContent.GetValueOrDefault("token");

                return Optional.OfNullable(token);
            }
        }

        public async Task<Optional<IEnumerable<GameforgeAccount>>> GetAllAccounts(string authToken, Guid installationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{URL}/user/accounts"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());
                request.Headers.Add("User-Agent", USER_AGENT);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                Trace.WriteLine(authToken);

                if (!response.IsSuccessStatusCode)
                {
                    return Optional.Empty<IEnumerable<GameforgeAccount>>();
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, GameforgeAccount> jsonContent = _serializer.Deserialize<Dictionary<string, GameforgeAccount>>(content);

                return Optional.Of<IEnumerable<GameforgeAccount>>(jsonContent.Values);
            }
        }

        public async Task<Optional<string>> GetSessionToken(string authToken, GameforgeAccount account, Guid installationId)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{URL}/auth/thin/codes"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                request.Headers.Add("TNT-Installation-Id", installationId.ToString());
                request.Headers.Add("User-Agent", USER_AGENT);

                string json = _serializer.Serialize(new SessionForm
                {
                    PlatformGameAccountId = account.Id
                });

                request.Content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);

                HttpResponseMessage response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return Optional.Empty<string>();
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> jsonContent = _serializer.Deserialize<Dictionary<string, string>>(content);

                string token = jsonContent.GetValueOrDefault("code");

                return Optional.OfNullable(token);
            }
        }
    }
}