using Newtonsoft.Json;

namespace NxtLib.ArbitraryMessageOperations
{
    public class DecryptedMessageReply : BaseReply
    {
        [JsonProperty(PropertyName = "decryptedMessage")]
        public string Message { get; set; }
    }
}