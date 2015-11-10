using System;

namespace NxtLib
{
    public class AccountWithPublicKey : Account, IEquatable<AccountWithPublicKey>
    {
        public BinaryHexString PublicKey { get; }

        public AccountWithPublicKey(string accountRs, BinaryHexString publicKey)
            : base(accountRs)
        {
            PublicKey = publicKey;
        }

        public AccountWithPublicKey(ulong accountId, BinaryHexString publicKey)
            : base(accountId)
        {
            PublicKey = publicKey;
        }

        public override bool Equals(object obj)
        {
            var other = obj as AccountWithPublicKey;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            return PublicKey.GetHashCode();
        }

        public bool Equals(AccountWithPublicKey other)
        {
            return other.PublicKey.Equals(PublicKey);
        }
    }
}