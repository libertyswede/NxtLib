using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.ServerInfoOperations
{
    public class GetPeerReply : BaseReply
    {
        public string AnnouncedAddress { get; set; }
        public string Application { get; set; }
        public bool Blacklisted { get; set; }
        public long DownloadedVolume { get; set; }
        public string Hallmark { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastUpdated { get; set; }
        public string Platform { get; set; }

        [JsonConverter(typeof(PeerStateConverter))]
        public PeerState State { get; set; }
        public bool ShareAddress { get; set; }
        public long UploadedVolume { get; set; }
        public string Version { get; set; }
        public int Weight { get; set; }

        public enum PeerState
        {
            NonConnected,
            Connected,
            Disconnected
        }
    }
}