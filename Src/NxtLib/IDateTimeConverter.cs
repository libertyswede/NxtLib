using System;

namespace NxtLib
{
    public interface IDateTimeConverter
    {
        int GetEpochTime(DateTime dateTime);
        DateTime GetFromNxtTime(int dateTime);
    }
}