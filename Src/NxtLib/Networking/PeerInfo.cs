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
        public bool Inbound { get; set; }
        public bool InboundWebSocket { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastUpdated { get; set; }
        public bool OutboundWebSocket { get; set; }
        public string Platform { get; set; }

        [JsonConverter(typeof(PeerStateConverter))]
        public PeerState State { get; set; }
        public bool ShareAddress { get; set; }
        public long UploadedVolume { get; set; }
        public string Version { get; set; }
        public int Weight { get; set; }

        public enum PeerState
        {
            [NxtApi("NON_CONNECTED")]
            NonConnected = 0,
            [NxtApi("CONNECTED")]
            Connected = 1,
            [NxtApi("DISCONNECTED")]
            Disconnected = 2
        }
    }
}