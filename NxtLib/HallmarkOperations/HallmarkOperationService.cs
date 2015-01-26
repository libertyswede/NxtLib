using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NxtLib.Internal;

namespace NxtLib.HallmarkOperations
{
    public interface IHallmarkOperationService
    {
        Task<DecodeHallmarkReply> DecodeHallmark(string hallmark);
        Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date);
    }

    public class HallmarkOperationService : BaseService, IHallmarkOperationService
    {
        public HallmarkOperationService(string baseAddress = DefaultBaseUrl)
            : base(new DateTimeConverter(), baseAddress)
        {
        }

        public HallmarkOperationService(IDateTimeConverter dateTimeConverter) : base(dateTimeConverter)
        {
        }

        public async Task<DecodeHallmarkReply> DecodeHallmark(string hallmark)
        {
            var queryParameters = new Dictionary<string, string> {{"hallmark", hallmark}};
            return await Get<DecodeHallmarkReply>("decodeHallmark", queryParameters);
        }

        public async Task<MarkHostReply> MarkHost(string secretPhrase, string host, int weight, DateTime date)
        {
            var queryParameters = new Dictionary<string, string>
            {
                {"secretPhrase", secretPhrase},
                {"host", host},
                {"weight", weight.ToString()},
                {"date", date.ToString("yyyy-MM-dd")}
            };
            return await Post<MarkHostReply>("markHost", queryParameters);
        }
    }
}
