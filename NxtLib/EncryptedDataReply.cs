using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib
{
    public class EncryptedDataReply : BaseReply
    {
        public string Data { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public IEnumerable<byte> Nonce { get; set; }

        public IEnumerable<byte> GetDataBytes()
        {
            return ByteToHexStringConverter.ToBytes(Data);
        }
    }
}