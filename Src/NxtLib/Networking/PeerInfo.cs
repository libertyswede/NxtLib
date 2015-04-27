using System;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.Networking
{
    public class PeerInfo
    {
        public string Address { get; set; }
        public string AnnouncedAddress { get; set; }
        public string Application { get; set; }
        public bool Blacklisted { get; set; }
        public string BlacklistingCause { get; set; }
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
            [Description("NON_CONNECTED")]
            NonConnected = 0,
            [Description("CONNECTED")]
            Connected = 1,
            [Description("DISCONNECTED")]
            Disconnected = 2
        }
    }
}