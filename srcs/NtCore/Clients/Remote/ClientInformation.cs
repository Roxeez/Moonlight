namespace NtCore.Clients.Remote
{
    public struct ClientInformation
    {
        public string DxHash { get; }
        public string GlHash { get; }
        public string Version { get; }

        public ClientInformation(string dxHash, string glHash, string version)
        {
            DxHash = dxHash;
            GlHash = glHash;
            Version = version;
        }
    }
}