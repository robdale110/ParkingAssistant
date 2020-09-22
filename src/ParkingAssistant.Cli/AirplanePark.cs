using System;
using ParkingAssistant.Cli.Interfaces;
using ParkingAssistant.Cli.Models;
using System.Collections.Generic;
using System.Linq;
using ParkingAssistant.Cli.Exceptions;

namespace ParkingAssistant.Cli
{
    public class AirplanePark : IAirplanePark
    {
        public IList<LargeSlot> LargeSlots { get; set; }
        public IList<MediumSlot> MediumSlots { get; set; }
        public IList<SmallSlot> SmallSlots { get; set; }

        public AirplanePark()
        {
            LargeSlots = new List<LargeSlot>();
            MediumSlots = new List<MediumSlot>();
            SmallSlots = new List<SmallSlot>();

            LargeSlots = Enumerable.Range(0, 25)
                .Select(largeSlot => new LargeSlot { IsAvailable = true })
                .ToList();
            MediumSlots = Enumerable.Range(0, 50)
                .Select(mediumSlot => new MediumSlot { IsAvailable = true })
                .ToList();
            SmallSlots = Enumerable.Range(0, 25)
                .Select(smallSlot => new SmallSlot { IsAvailable = true })
                .ToList();
        }

        public Slot FindSlot(Airplane plane)
        {
            if (plane == null)
            {
                throw new ArgumentNullException(nameof(plane));
            }

            Slot availableSlot = plane switch
            {
                Jumbo jumbo => LargeSlots.ToList().Find(slot => slot.IsAvailable),
                Jet jet => MediumSlots.ToList().Find(slot => slot.IsAvailable),
                Prop prop => SmallSlots.ToList().Find(slot => slot.IsAvailable),
                _ => throw new UnknownPlaneException("Plane type not recognised")
            };

            if (availableSlot == null)
            {
                throw new NoRequiredSlotException("There are no slots available for this type of plane");
            }

            return availableSlot;
        }
            

        public void BookSlot(Slot slot, Airplane plane)
        {
            // Similar to above, but make one as unavailable
            // Maybe add name to plane and actually add the class to the collection
        }
    }
}
