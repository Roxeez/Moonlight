using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NtCore.Clients.Remote
{
    public interface IGameforgeAuth
    {
        Task<Account> GetAccount(string username, string password, Language language);
        Task<string> GetSessionToken(Account account, Guid installationId = default);
    }
}