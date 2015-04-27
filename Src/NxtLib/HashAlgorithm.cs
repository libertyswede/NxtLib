using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum HashAlgorithm
    {
        [Description("SHA256")]
        Sha256 = 2,
        [Description("SHA3")]
        Sha3 = 3,
        [Description("SCRYPT")]
        Scrypt = 5,
        [Description("RIPEMD160")]
        Ripemd160 = 6,
        [Description("Keccak25")]
        Keccak25 = 25,
        [Description("RIPEMD160_SHA256")]
        Ripemd160Sha256 = 62
    }
}