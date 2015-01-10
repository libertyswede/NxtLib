using Newtonsoft.Json;

namespace NxtLib.ArbitraryMessageOperations
{
    public class DecryptedMessage : BaseReply
    {
        [JsonProperty(PropertyName = "decryptedMessage")]
        public string Message { get; set; }
    }
}