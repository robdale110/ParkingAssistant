using System;

namespace ParkingAssistant.Cli.Exceptions
{
    public class UnknownPlaneException : Exception
    {
        public UnknownPlaneException(string message) : base(message) { }
    }
}
