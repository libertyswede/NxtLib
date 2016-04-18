using System;

namespace NxtLib.Local
{
    public class Constants
    {
        public const string NxtVersionSupport = "1.8.2";
        public static readonly Account GenesisAccount = 1739068987193023818;
        public static readonly DateTime EpochBeginning = new DateTime(2013, 11, 24, 12, 0, 0, DateTimeKind.Utc);
        public const ulong GenesisBlockId = 2680262203532249785;

        public const string DefaultNxtUrl = "http://localhost:7876/nxt";
        public const string TestnetNxtUrl = "http://localhost:6876/nxt";
    }
}
