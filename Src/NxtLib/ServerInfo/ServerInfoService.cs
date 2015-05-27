﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public async Task<EventRegisterReply> EventRegister(NxtEvent? nxtEvent = null, bool? add = null, bool? remove = null)
        {
            var queryParameters = new Dictionary<string, List<string>>();
            if (nxtEvent.HasValue)
            {
                queryParameters.Add("event", GetEventList(nxtEvent.Value));
            }
            if (add.HasValue)
            {
                queryParameters.Add("add", new List<string>{add.ToString()});
            }
            if (remove.HasValue)
            {
                queryParameters.Add("remove", new List<string> {remove.ToString()});
            }
            return await Post<EventRegisterReply>("eventRegister", queryParameters);
        }

        private static List<string> GetEventList(NxtEvent nxtEvent)
        {
            var events = from NxtEvent flag in Enum.GetValues(typeof (NxtEvent))
                where nxtEvent.HasFlag(flag)
                select
                    flag.GetType()
                        .GetTypeInfo()
                        .GetDeclaredField(flag.ToString())
                        .GetCustomAttribute<NxtApiAttribute>()
                into apiAttribute
                select apiAttribute.Name;
            
            return events.ToList();
        }
        
        public async Task<EventWaitReply> EventWait(long timeout)
        {
            var queryParameters = new Dictionary<string, string> {{"timeout", timeout.ToString()}};
            return await Post<EventWaitReply>("eventWait", queryParameters);
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
