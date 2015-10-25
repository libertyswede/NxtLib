using System;

namespace NxtLib.Internal.LocalSign
{
    internal class InvalidReedSolomonAddressException : ArgumentException
    {
        public InvalidReedSolomonAddressException(string addressRs, string paramName)
            : base($"The string \"{addressRs}\" is not a valid reed solomon address", paramName)
        {
        }
    }
}