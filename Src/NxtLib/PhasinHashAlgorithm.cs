using System.Diagnostics;

namespace NxtLib
{
    [DebuggerDisplay("{ToString()} - {GetHashCode()}")]
    public enum PhasingHashAlgorithm
    {
        [NxtApi("SHA256")]
        Sha256 = 2,
        [NxtApi("RIPEMD160")]
        Ripemd160 = 6,
        [NxtApi("RIPEMD160_SHA256")]
        Ripemd160Sha256 = 62
    }
}