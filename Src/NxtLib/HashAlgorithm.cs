using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum HashAlgorithm
    {
        Sha256 = 2,
        Sha3 = 3,
        Scrypt = 5,
        Ripemd160 = 6,
        Keccak25 = 25,
        Ripemd160Sha256 = 62
    }
}