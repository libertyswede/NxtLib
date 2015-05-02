using System.Collections.Generic;

namespace NxtLib.VotingSystem
{
    public class CreatePollParameters
    {
        public string Description { get; set; }
        public int FinishHeight { get; set; }
        public ulong? HoldingId { get; set; }
        public int MaxNumberOfOptions { get; set; }
        public int MaxRangeValue { get; set; }
        public long? MinBalance { get; set; }
        public MinBalanceModel? MinBalanceModel { get; set; }
        public int MinNumberOfOptions { get; set; }
        public int MinRangeValue { get; set; }
        public List<string> Options { get; set; }
        public string Name { get; set; }
        public VotingModel VotingModel { get; set; }

        public CreatePollParameters(string name, string description, int finishHeight,
            VotingModel votingModel, int minNumberOfOptions, int maxNumberOfOptions, int minRangeValue,
            int maxRangeValue, List<string> options)
        {
            Name = name;
            Description = description;
            FinishHeight = finishHeight;
            VotingModel = votingModel;
            MinNumberOfOptions = minNumberOfOptions;
            MaxNumberOfOptions = maxNumberOfOptions;
            MinRangeValue = minRangeValue;
            MaxRangeValue = maxRangeValue;
            Options = options;
        }
    }
}