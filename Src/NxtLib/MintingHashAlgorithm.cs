using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum MintingHashAlgorithm
    {
        [NxtApi("SHA256")]
        Sha256 = 2,
        [NxtApi("SHA3")]
        Sha3 = 3,
        [NxtApi("SCRYPT")]
        Scrypt = 5,
        [NxtApi("Keccak25")]
        Keccak25 = 25,
    }
}