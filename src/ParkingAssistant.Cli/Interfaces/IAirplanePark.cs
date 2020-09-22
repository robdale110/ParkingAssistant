using ParkingAssistant.Cli.Models;
using System.Collections.Generic;

namespace ParkingAssistant.Cli.Interfaces
{
    public interface IAirplanePark
    {
        IList<LargeSlot> LargeSlots { get; set; }
        IList<MediumSlot> MediumSlots { get; set; }
        IList<SmallSlot> SmallSlots { get; set; }
        Slot FindSlot(Airplane plane);
        void BookSlot(Slot slot, Airplane plane);
    }
}
