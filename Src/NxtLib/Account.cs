using System;
using System.Diagnostics;
using NxtLib.Local;

namespace NxtLib
{
    [DebuggerDisplay("Account RS: {AccountRs}")]
    public class Account : IEquatable<Account>
    {
        private static readonly LocalCrypto LocalCrypto = new LocalCrypto();
        public ulong AccountId { get; }
        public string AccountRs { get; }

        public Account(string accountRs)
        {
            AccountId = LocalCrypto.GetAccountIdFromReedSolomon(accountRs);
            AccountRs = accountRs;
        }

        public Account(ulong accountId)
        {
            AccountRs = LocalCrypto.GetReedSolomonFromAccountId(accountId);
            AccountId = accountId;
        }

        public static implicit operator Account(string accountRs)
        {
            return new Account(accountRs);
        }

        public static implicit operator Account(ulong accountId)
        {
            return new Account(accountId);
        }

        public static implicit operator Account(long accountId)
        {
            return new Account((ulong)accountId);
        }

        public override int GetHashCode()
        {
            return AccountRs.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Account;
            return other != null && Equals(other);
        }

        public bool Equals(Account other)
        {
            return other?.AccountId == AccountId;
        }
    }
}
