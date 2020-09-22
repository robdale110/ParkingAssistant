using FluentAssertions;
using ParkingAssistant.Cli.Exceptions;
using ParkingAssistant.Cli.Interfaces;
using ParkingAssistant.Cli.Models;
using System;
using System.Linq;
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
        public void FindSlot_Should_Throw_NoRequiredSlotException_If_There_Are_No_LargeSlots_Available()
        {
            AirplaneParkTestHelper.CreateFullLargeSlots(_airplanePark);
            Action result = () => { _ = _airplanePark.FindSlot(new Jumbo()); };
            result.Should().Throw<NoRequiredSlotException>();
        }

        [Fact]
        public void FindSlot_Should_Throw_NoRequiredSlotException_If_There_Are_No_MediumSlots_Available()
        {
            AirplaneParkTestHelper.CreateFullMediumSlots(_airplanePark);
            Action result = () => { _ = _airplanePark.FindSlot(new Jet()); };
            result.Should().Throw<NoRequiredSlotException>();
        }

        [Fact]
        public void FindSlot_Should_Throw_NoRequiredSlotException_If_There_Are_No_SmallSlots_Available()
        {
            AirplaneParkTestHelper.CreateFullSmallSlots(_airplanePark);
            Action result = () => { _ = _airplanePark.FindSlot(new Prop()); };
            result.Should().Throw<NoRequiredSlotException>();
        }

        [Fact]
        public void BookSlot_Should_Throw_ArgumentNullException_If_Slot_Not_Provided()
        {
            Action result = () => { _airplanePark.BookSlot(null, new Airplane()); };
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BookSlot_Should_Throw_ArgumentNullException_If_Airplane_Not_Provided()
        {
            Action result = () => { _airplanePark.BookSlot(new Slot(), null); };
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void BookSlot_Can_Put_Correct_Airplane_In_Large_Slot()
        {
            var jumbo = new Jumbo { Name = "A380" };
            var slot = _airplanePark.FindSlot(jumbo);
            _airplanePark.BookSlot(slot, jumbo);
            _airplanePark.LargeSlots.Count(theSlot => !theSlot.IsAvailable).Should().Be(1);
        }

        [Fact]
        public void BookSlot_Can_Put_Correct_Airplane_In_Medium_Slot()
        {
            var jet = new Jet { Name = "A330" };
            var slot = _airplanePark.FindSlot(jet);
            _airplanePark.BookSlot(slot, jet);
            _airplanePark.MediumSlots.Count(theSlot => !theSlot.IsAvailable).Should().Be(1);
        }

        [Fact]
        public void BookSlot_Can_Put_Correct_Airplane_In_Small_Slot()
        {
            var prop = new Prop { Name = "E195" };
            var slot = _airplanePark.FindSlot(prop);
            _airplanePark.BookSlot(slot, prop);
            _airplanePark.SmallSlots.Count(theSlot => !theSlot.IsAvailable).Should().Be(1);
        }
    }
}
