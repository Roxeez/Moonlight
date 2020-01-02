using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NtCore.Core;

namespace NtCore.Services.Gameforge
{
    public interface IGameforgeAuthService
    {
        Task<Optional<string>> GetAuthToken(string username, string password, Language language);
        Task<Optional<IEnumerable<GameforgeAccount>>> GetAllAccounts(string authToken, Guid installationId);
        Task<Optional<string>> GetSessionToken(string authToken, GameforgeAccount account, Guid installationId);
    }
}