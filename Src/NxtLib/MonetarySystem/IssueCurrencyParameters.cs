using System.Collections.Generic;
using System.Linq;
using NxtLib.Internal;

namespace NxtLib.MonetarySystem
{
    public class IssueCurrencyParameters
    {
        public HashAlgorithm? Algorithm { get; set; }
        public string Code { get; set; }
        public byte? Decimals { get; set; }
        public string Description { get; set; }
        public long? InitialSupply { get; set; }
        public int? IssuanceHeight { get; set; }
        public int? MaxDifficulty { get; set; }
        public long? MaxSupply { get; set; }
        public int? MinDifficulty { get; set; }
        public Amount MinReservePerUnit { get; set; }
        public string Name { get; set; }
        public long? ReserveSupply { get; set; }
        public byte? Ruleset { get; set; }
        public HashSet<CurrencyType> Types { get; set; }

        public IssueCurrencyParameters()
        {
            Types = new HashSet<CurrencyType>();
        }

        internal void AppendToQueryParameters(Dictionary<string, string> queryParameters)
        {
            if (Algorithm.HasValue)
            {
                queryParameters.Add(Parameters.Algorithm, ((int)Algorithm.Value).ToString());
            }
            queryParameters.AddIfHasValue(Parameters.Code, Code);
            queryParameters.AddIfHasValue(Parameters.Decimals, Decimals);
            queryParameters.AddIfHasValue(Parameters.Description, Description);
            queryParameters.AddIfHasValue(Parameters.InitialSupply, InitialSupply);
            queryParameters.AddIfHasValue(Parameters.IssuanceHeight, IssuanceHeight);
            queryParameters.AddIfHasValue(Parameters.MaxDifficulty, MaxDifficulty);
            queryParameters.AddIfHasValue(Parameters.MaxSupply, MaxSupply);
            queryParameters.AddIfHasValue(Parameters.MinDifficulty, MinDifficulty);
            if (MinReservePerUnit != null)
            {
                queryParameters.AddIfHasValue(Parameters.MinReservePerUnitNqt, MinReservePerUnit.Nqt.ToString());
            }

            queryParameters.AddIfHasValue(Parameters.Name, Name);
            queryParameters.AddIfHasValue(Parameters.ReserveSupply, ReserveSupply);
            queryParameters.AddIfHasValue(Parameters.Ruleset, Ruleset);
            queryParameters.AddIfHasValue(Parameters.Type, AggregateTypes());
        }

        private int? AggregateTypes()
        {
            var currencyType = Types.Any() ? 0 : (int?)null;
            return Types.Aggregate(currencyType, (current, type) => current | (int) type);
        }
    }
}