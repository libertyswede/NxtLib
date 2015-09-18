namespace NxtLib.ServerInfo
{
    public class RequestType
    {
        public bool AllowRequiredBlockParameters { get; set; }
        public string Name { get; set; }
        public bool RequireBlockchain { get; set; }
        public bool RequirePassword { get; set; }
        public bool RequirePost { get; set; }
    }
}