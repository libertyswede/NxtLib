using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;
using NxtLib.Local;

namespace NxtLib.ServerInfo
{
    public class ServerInfoService : BaseService, IServerInfoService
    {
        public ServerInfoService(string baseAddress = Constants.DefaultNxtUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public ServerInfoService(IDateTimeConverter dateTimeConverter) 
            : base(dateTimeConverter)
        {
        }

        public async Task<object> EventRegister(List<string> events, string add, string remove)
        {
            throw new NotImplementedException("Not enough documentation about this function exist yet");
        }

        //Block.BLOCK_GENERATED
        //Block.BLOCK_POPPED
        //Block.BLOCK_PUSHED
        //Peer.ADD_INBOUND
        //Peer.ADDED_ACTIVE_PEER
        //Peer.BLACKLIST
        //Peer.CHANGED_ACTIVE_PEER
        //Peer.DEACTIVATE
        //Peer.NEW_PEER
        //Peer.REMOVE
        //Peer.REMOVE_INBOUND
        //Peer.UNBLACKLIST
        //Transaction.ADDED_CONFIRMED_TRANSACTIONS
        //Transaction.ADDED_UNCONFIRMED_TRANSACTIONS
        //Transaction.REJECT_PHASED_TRANSACTION
        //Transaction.RELEASE_PHASED_TRANSACTION
        //Transaction.REMOVE_UNCONFIRMED_TRANSACTIONS

        // Sample response on eventWait:

        //{
        //    "requestProcessingTime": 0,
        //    "events": [
        //        {
        //            "name": "Peer.DEACTIVATE",
        //            "ids": [
        //                "74.74.83.190"
        //            ]
        //        }
        //    ]
        //}
        public async Task<object> EventWait(long timeout)
        {
            throw new NotImplementedException("Not enough documentation about this function exist yet");
        }

        public async Task<GetBlockchainStatusReply> GetBlockchainStatus()
        {
            return await Get<GetBlockchainStatusReply>("getBlockchainStatus");
        }

        public async Task<GetConstantsReply> GetConstants()
        {
            return await Get<GetConstantsReply>("getConstants");
        }

        public async Task<GetPluginsReply> GetPlugins()
        {
            return await Get<GetPluginsReply>("getPlugins");
        }

        public async Task<GetStateReply> GetState(bool? includeCounts = null)
        {
            var queryParameters = new Dictionary<string, string>();
            if (includeCounts.HasValue)
            {
                queryParameters = new Dictionary<string, string> {{"includeCounts", includeCounts.Value.ToString()}};
            }
            return await Get<GetStateReply>("getState", queryParameters);
        }

        public async Task<GetTimeReply> GetTime()
        {
            return await Get<GetTimeReply>("getTime");
        }
    }
}
