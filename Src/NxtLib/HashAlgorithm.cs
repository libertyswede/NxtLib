using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum HashAlgorithm
    {
        [NxtApi("SHA256")]
        Sha256 = 2,
        [NxtApi("SHA3")]
        Sha3 = 3,
        [NxtApi("SCRYPT")]
        Scrypt = 5,
        [NxtApi("RIPEMD160")]
        Ripemd160 = 6,
        [NxtApi("Keccak25")]
        Keccak25 = 25,
        [NxtApi("RIPEMD160_SHA256")]
        Ripemd160Sha256 = 62
    }
}