using FluentAssertions;
using ParkingAssistant.Cli.Exceptions;
using ParkingAssistant.Cli.Interfaces;
using ParkingAssistant.Cli.Models;
using System;
using Xunit;

namespace ParkingAssistant.Cli.Tests
{
    public class AirplaneParkTests
    {
        private readonly IAirplanePark _airplanePark;

        public AirplaneParkTests()
        {
            _airplanePark = new AirplanePark();
        }

        [Fact]
        public void Create_Should_Have_25_Large_Slots()
        {
            _airplanePark.LargeSlots.Count.Should().Be(25);
        }

        [Fact]
        public void Create_Should_Have_50_Medium_Slots()
        {
            _airplanePark.MediumSlots.Count.Should().Be(50);
        }

        [Fact]
        public void Create_Should_Have_25_Small_Slots()
        {
            _airplanePark.SmallSlots.Count.Should().Be(25);
        }

        [Fact]
        public void FindSlot_Should_Recommend_Large_Slot_For_Jumbo()
        {
            var result = _airplanePark.FindSlot(new Jumbo());

            result.Should().BeOfType<LargeSlot>();
        }

        [Fact]
        public void FindSlot_Should_Recommend_Small_Slot_For_Prop()
        {
            var result = _airplanePark.FindSlot(new Prop());

            result.Should().BeOfType<SmallSlot>();
        }

        [Fact]
        public void FindSlot_Should_Recommend_Medium_Slot_For_Jet()
        {
            var result = _airplanePark.FindSlot(new Jet());

            result.Should().BeOfType<MediumSlot>();
        }

        [Fact]
        public void FindSlot_Should_Throw_ArgumentNullException_If_No_Airplane_Provided()
        {
            Action result = () => { _ = _airplanePark.FindSlot(null); };
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FindSlot_Should_Throw_UnknownAirplaneException_If_Type_Not_Recognised()
        {
            Action result = () => { _ = _airplanePark.FindSlot(new InvalidPlane()); };
            result.Should().Throw<UnknownPlaneException>();
        }

        [Fact]
        public void FindSlot_Should_Throw_NoRequiredSlotException_If_There_Are_No_Slots_Of_Required_Type()
        {
            AirplaneParkTestHelper.CreateFullLargeSlots();
            Action result = () => { _ = _airplanePark.FindSlot(new Jumbo()); };
            result.Should().Throw<UnknownPlaneException>();
        }
    }
}
