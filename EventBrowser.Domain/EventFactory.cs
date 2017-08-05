using System.Collections.Generic;

namespace EventBrowser.Domain
{
    public static class EventFactory
    {
        public static Event AbsoluteEvent(string name, ApproximateDateTime timelineDateTime)
        {
            return new Event(name, new EventRelationship.Absolute(timelineDateTime));
        }

        public static Event AfterEvent(string name, Event afterEvent, ApproximateDateTimeOffset offset)
        {
            return new Event(name, new EventRelationship.After(afterEvent, offset));
        }

        public static Event BeforeEvent(string name, Event beforeEvent, ApproximateDateTimeOffset offset)
        {
            return new Event(name, new EventRelationship.Before(beforeEvent, offset));
        }

        public static Event BetweenEvents(string name, Event afterEvent, ApproximateDateTimeOffset afterEventOffset, Event beforeEvent, ApproximateDateTimeOffset beforeEventOffset)
        {
            var relationships = new List<EventRelationship>();
            relationships.Add(new EventRelationship.After(afterEvent, afterEventOffset));
            relationships.Add(new EventRelationship.Before(beforeEvent, beforeEventOffset));

            return new Event(name, relationships);
        }

        public static Event ConcurrentEvent(string name, Event concurrentEvent, ApproximateDateTimeOffset offset)
        {
            return new Event(name, new EventRelationship.Concurrent(concurrentEvent, offset));
        }
    }

}
