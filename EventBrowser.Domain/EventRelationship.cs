using System;

namespace EventBrowser.Domain
{
    public abstract class EventRelationship
    {
        public readonly EventRelationshipType Type;

        private EventRelationship(EventRelationshipType type)
        {
            Type = type;
        }

        public class Absolute : EventRelationship
        {
            public ApproximateDateTime AbsoluteDateTime { get; set; }

            public Absolute(ApproximateDateTime date) : base(EventRelationshipType.Absolute)
            {
                AbsoluteDateTime = date;
            }
        }

        public class After : EventRelationship
        {
            public Guid AfterEventId { get; set; }
            public Event AfterEvent { get; set; }
            public ApproximateDateTimeOffset DateTimeOffset { get; set; }

            public After(Event afterEvent, ApproximateDateTimeOffset offset) : base(EventRelationshipType.After)
            {
                AfterEventId = afterEvent.Id;
                AfterEvent = afterEvent;
                DateTimeOffset = offset;
            }
        }

        public class Before : EventRelationship
        {
            public Guid BeforeEventId { get; set; }
            public Event BeforeEvent { get; set; }
            public ApproximateDateTimeOffset DateTimeOffset { get; set; }

            public Before(Event beforeEvent, ApproximateDateTimeOffset offset) : base(EventRelationshipType.Before)
            {
                BeforeEventId = beforeEvent.Id;
                BeforeEvent = beforeEvent;
                DateTimeOffset = offset;
            }
        }
        
        public class Concurrent : EventRelationship
        {
            public Guid ConcurrentEventId { get; set; }
            public Event ConcurrentEvent { get; set; }
            public ApproximateDateTimeOffset DateTimeOffset { get; set; }

            public Concurrent(Event concurrentEvent, ApproximateDateTimeOffset offset) : base(EventRelationshipType.Concurrent)
            {
                ConcurrentEventId = concurrentEvent.Id;
                ConcurrentEvent = concurrentEvent;
                DateTimeOffset = offset;
            }
        }
    }
    
    public enum EventRelationshipType
    {
        Absolute = 1,
        Before = 2,
        After = 3,
        Concurrent = 4
    }
}
