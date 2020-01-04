using JetBrains.Annotations;
using NtCore.Services.Gameforge;

namespace NtCore.Clients.Remote
{
    public sealed class LoginServer : Server
    {
        private const string GameforgeLoginServerIp = "79.110.84.75";
        
        public static readonly LoginServer EN = new LoginServer(GameforgeLoginServerIp, 4000, Language.EN);
        public static readonly LoginServer DE = new LoginServer(GameforgeLoginServerIp, 4001, Language.DE);
        public static readonly LoginServer FR = new LoginServer(GameforgeLoginServerIp, 4002, Language.FR);
        public static readonly LoginServer IT = new LoginServer(GameforgeLoginServerIp, 4003, Language.IT);
        public static readonly LoginServer PL = new LoginServer(GameforgeLoginServerIp, 4004, Language.PL);
        public static readonly LoginServer ES = new LoginServer(GameforgeLoginServerIp, 4005, Language.ES);

        public Language Language { get; }
        
        private LoginServer([NotNull] string ip, short port, Language language) : base(ip, port)
        {
        }
    }
}