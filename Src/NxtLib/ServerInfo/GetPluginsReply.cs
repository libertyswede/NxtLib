using System.Collections.Generic;

namespace NxtLib.ServerInfo
{
    public class GetPluginsReply : BaseReply
    {
        public List<string> Plugins { get; set; }
    }
}