using System.Collections.Generic;

namespace NxtLib.ServerInfo
{
    public class TransactionTypeBase
    {
        public string Description { get; set; }
        public byte Value { get; set; }
    }

    // Intentionally left blank
    public class TransactionSubType : TransactionTypeBase
    {
    }

    public class TransactionType : TransactionTypeBase
    {
        public List<TransactionSubType> Subtypes { get; set; }
    }
}