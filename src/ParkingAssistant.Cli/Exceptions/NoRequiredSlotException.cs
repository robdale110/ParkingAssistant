using System;

namespace ParkingAssistant.Cli.Exceptions
{
    public class NoRequiredSlotException : Exception
    {
        public NoRequiredSlotException(string message) : base(message) { }
    }
}
