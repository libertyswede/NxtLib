using System.Collections.Generic;
using System.Linq;
using NxtLib.Internal;

namespace NxtLib.MonetarySystemOperations
{
    public class IssueCurrencyParameters
    {
        public byte? Algorithm { get; set; }
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
            queryParameters.AddIfHasValue("algorithm", Algorithm);
            queryParameters.AddIfHasValue("code", Code);
            queryParameters.AddIfHasValue("decimals", Decimals);
            queryParameters.AddIfHasValue("description", Description);
            queryParameters.AddIfHasValue("initialSupply", InitialSupply);
            queryParameters.AddIfHasValue("issuanceHeight", IssuanceHeight);
            queryParameters.AddIfHasValue("maxDifficulty", MaxDifficulty);
            queryParameters.AddIfHasValue("maxSupply", MaxSupply);
            queryParameters.AddIfHasValue("minDifficulty", MinDifficulty);
            if (MinReservePerUnit != null)
            {
                queryParameters.AddIfHasValue("minReservePerUnitNQT", MinReservePerUnit.Nqt.ToString());
            }

            queryParameters.AddIfHasValue("name", Name);
            queryParameters.AddIfHasValue("reserveSupply", ReserveSupply);
            queryParameters.AddIfHasValue("ruleset", Ruleset);
            queryParameters.AddIfHasValue("type", AggregateTypes());
        }

        private int? AggregateTypes()
        {
            var currencyType = Types.Any() ? 0 : (int?)null;
            return Types.Aggregate(currencyType, (current, type) => current | (int) type);
        }
    }
}