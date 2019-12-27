using System;
using System.Threading.Tasks;
using NtCore.Core;

namespace NtCore.Services.Gameforge
{
    public interface IGameforgeAuthService
    {
        Task<Optional<GameforgeAccount>> Connect(string username, string password, Language language);
        Task<Optional<string>> GetToken(GameforgeAccount account, Guid installationId = default);
    }
}