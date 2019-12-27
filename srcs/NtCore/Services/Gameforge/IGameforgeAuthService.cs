using System;
using System.Threading.Tasks;

namespace NtCore.Services.Gameforge
{
    public interface IGameforgeAuthService
    {
        Task<GameforgeAccount> GetAccount(string username, string password, Language language);
        Task<string> GetToken(GameforgeAccount account, Guid installationId = default);
    }
}