using Newtonsoft.Json;

namespace NxtLib.Messages
{
    public class DecryptedMessageReply : BaseReply
    {
        [JsonProperty(PropertyName = "decryptedMessage")]
        public string Message { get; set; }
    }
}