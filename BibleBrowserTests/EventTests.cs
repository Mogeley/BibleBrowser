using EventBrowser.Domain;
using System;
using Xunit;

namespace BibleBrowserTests
{
    public class EventTests : TestBase
    {
        [Fact]
        public void Absolute_Event_Test()
        {
            // Expectations
            var firstDate = new ApproximateDateTime();

            // Create First Event
            Event firstEvent = EventFactory.AbsoluteEvent("First Event", firstDate);

            Assert.True(firstEvent.EventDate.Equals(firstDate), $"Expected ApproximateDateTime of {firstDate}, recieved: {firstEvent.EventDate}");
        }

        [Fact]
        public void After_Event_Test()
        {
            // Expectations
            var firstDate = new ApproximateDateTime();
            var afterDate = new ApproximateDateTime(1);

            // Create First Event
            Event firstEvent = EventFactory.AbsoluteEvent("First Event", firstDate);

            // Create event 1 year after first event
            Event afterEvent = EventFactory.AfterEvent("Second Event - After First Event", firstEvent, new ApproximateDateTimeOffset(years: 1));

            Assert.True(firstEvent.EventDate.Equals(firstDate), $"Expected ApproximateDateTime of {firstDate}, recieved: {firstEvent.EventDate}");
            Assert.True(afterEvent.EventDate.Equals(afterDate), $"Expected ApproximateDateTime of {afterDate}, recieved: {afterEvent.EventDate}");
        }

        [Fact]
        public void Before_Event_Test()
        {
            // Expectations
            var firstDate = new ApproximateDateTime(1);
            var beforeDate = new ApproximateDateTime(0, 11);

            // Create First Event
            Event firstEvent = EventFactory.AbsoluteEvent("First Event", firstDate);

            // Create event 1 month before firstEvent
            Event beforeEvent = EventFactory.BeforeEvent("Before Event - Before First Event", firstEvent, new ApproximateDateTimeOffset(months: 1));

            Assert.True(firstEvent.EventDate.Equals(firstDate), $"Expected ApproximateDateTime of {firstDate}, recieved: {firstEvent.EventDate}");
            Assert.True(beforeEvent.EventDate.Equals(beforeDate), $"Expected ApproximateDateTime of {beforeDate}, recieved: {beforeEvent.EventDate}");
        }

        [Fact]
        public void Exact_Date_Between_Events_Test()
        {
            // Expectations
            var firstDate = new ApproximateDateTime();
            var lastDate = new ApproximateDateTime(3);
            var betweenDate = new ApproximateDateTime(1);

            // Create First / Last Events
            Event firstEvent = EventFactory.AbsoluteEvent("First Event", firstDate);
            Event lastEvent = EventFactory.AbsoluteEvent("Last Event", lastDate);

            // Create Between Event
            Event betweenEvent = EventFactory.BetweenEvents("Between Event", firstEvent, new ApproximateDateTimeOffset(years: 1), lastEvent, new ApproximateDateTimeOffset(years: 2));

            Assert.True(firstEvent.EventDate.Equals(firstDate), $"Expected ApproximateDateTime of {firstDate}, recieved: {firstEvent.EventDate}");
            Assert.True(lastEvent.EventDate.Equals(lastDate), $"Expected ApproximateDateTime of {lastDate}, recieved: {lastEvent.EventDate}");
            Assert.True(betweenEvent.EventDate.Equals(betweenDate), $"Expected ApproximateDateTime of {betweenDate}, recieved: {betweenEvent.EventDate}");
        }

        [Fact]
        public void Concurrent_Event_Test()
        {

        }
    }
}
