using System;

namespace NxtLib.Local
{
    internal class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ValidationException(string propertyName, object expectedValue, object actualValue)
            : base($"Parameter {propertyName} do not match. Expected: {expectedValue} Actual: {actualValue}")
        {
        }
    }
}