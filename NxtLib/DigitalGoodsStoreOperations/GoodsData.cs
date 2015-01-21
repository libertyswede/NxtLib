using System.Collections.Generic;
using Newtonsoft.Json;
using NxtLib.Internal;

namespace NxtLib.DigitalGoodsStoreOperations
{
    public class GoodsData
    {
        public string Data { get; set; }

        [JsonConverter(typeof(ByteToHexStringConverter))]
        public IEnumerable<byte> Nonce { get; set; }
    }
}