using ParkingAssistant.Cli.Interfaces;
using ParkingAssistant.Cli.Models;
using System.Linq;

namespace ParkingAssistant.Cli.Tests
{
    public class AirplaneParkTestHelper
    {
        public static void CreateFullLargeSlots(IAirplanePark airplanePark) =>
            airplanePark.LargeSlots = Enumerable.Range(0, 25)
                .Select(largeSlot => new LargeSlot { IsAvailable = false })
                .ToList();

        public static void CreateFullMediumSlots(IAirplanePark airplanePark) =>
            airplanePark.MediumSlots = Enumerable.Range(0, 25)
                .Select(largeSlot => new MediumSlot { IsAvailable = false })
                .ToList();

        public static void CreateFullSmallSlots(IAirplanePark airplanePark) =>
            airplanePark.SmallSlots = Enumerable.Range(0, 25)
                .Select(largeSlot => new SmallSlot { IsAvailable = false })
                .ToList();
    }
}
