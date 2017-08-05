using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBrowser.Domain
{
    /// <summary>
    /// Describes a specific event in time and associates all other information about the event to this instance
    /// </summary>
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ApproximateDateTime EventDate { get; set; }
        public ApproximateDateSpan DateRange { get; set; }

        public List<EventRelationship> Relationships = new List<EventRelationship>();
        
        public Event(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Event(string name, EventRelationship relationship)
        {
            Id = Guid.NewGuid();
            Name = name;
            Relationships.Add(relationship);
            SetEventDate();
        }

        public Event(string name, List<EventRelationship> relationships)
        {
            Id = Guid.NewGuid();
            Name = name;
            Relationships = relationships;
            SetEventDate();
        }

        public void SetEventDate()
        {
            // get Event Date from relationships 
            if(Relationships.Any(r => r.Type == EventRelationshipType.Absolute))
            {
                var relationship = (EventRelationship.Absolute)Relationships.Where(r => r.Type == EventRelationshipType.Absolute).First();

                EventDate = relationship.AbsoluteDateTime;
            }
            // Before and After relationships
            else if (Relationships.Any(r => r.Type == EventRelationshipType.After) && Relationships.Any(r => r.Type == EventRelationshipType.Before))
            {
                // can have no offsets, one offset, both offsets resulting in date range / exact date or overlapping dates
                var after = (EventRelationship.After)Relationships.Where(r => r.Type == EventRelationshipType.After).First();
                var afterDate = CalculateAfter(after);
                //= after.AfterEvent.EventDate.Copy; // It's important to copy the EventDate otherwise we will mutate the parent EventDate instead of a new event Date!
                //afterDate.AddOffset(after.DateTimeOffset);

                var before = (EventRelationship.Before)Relationships.Where(r => r.Type == EventRelationshipType.Before).First();
                var beforeDate = CalculateBefore(before);
                //before.BeforeEvent.EventDate.Copy;
                //beforeDate.SubtractOffset(before.DateTimeOffset);

                // case: before and after result in single date
                if (afterDate.Equals(beforeDate))
                {
                    EventDate = afterDate;
                }
                else // case: before and after result in date range
                {
                    if (afterDate.LessThan(beforeDate))
                    {
                        DateRange = new ApproximateDateSpan(afterDate, beforeDate);
                    }
                    else
                    {
                        DateRange = new ApproximateDateSpan(beforeDate, afterDate);
                    }
                }

            }
            // Only After Relationship - note may need to consider multiple after relationships...
            else if (Relationships.Any(r => r.Type == EventRelationshipType.After)) // this event is after the afterEvent with offset
            {
                var relationship = (EventRelationship.After)Relationships.Where(r => r.Type == EventRelationshipType.After).First();

                EventDate = CalculateAfter(relationship);

                //EventDate = relationship.AfterEvent.EventDate.Copy;
                //EventDate.AddOffset(relationship.DateTimeOffset);
            }
            // Only Before Relationship
            else if (Relationships.Any(r => r.Type == EventRelationshipType.Before)) // this event is before the beforeEvent with offset
            {
                var relationship = (EventRelationship.Before)Relationships.Where(r => r.Type == EventRelationshipType.Before).First();

                EventDate = CalculateBefore(relationship);
            }
            // Only Concurrent relationship
            else if (Relationships.Any(r => r.Type == EventRelationshipType.Concurrent)) // this event is concurrent
            {
                var relationship = (EventRelationship.Concurrent)Relationships.Where(r => r.Type == EventRelationshipType.Concurrent).First();

                EventDate = CalculateConcurrent(relationship);

                //EventDate = relationship.ConcurrentEvent.EventDate.Copy;
                //EventDate.AddOffset(relationship.DateTimeOffset);
            }
        }

        private ApproximateDateTime CalculateAfter(EventRelationship.After relationship) =>
            relationship.AfterEvent.EventDate.Copy.AddOffset(relationship.DateTimeOffset);

        private ApproximateDateTime CalculateConcurrent(EventRelationship.Concurrent relationship) =>
            relationship.ConcurrentEvent.EventDate.Copy.AddOffset(relationship.DateTimeOffset);

        private ApproximateDateTime CalculateBefore(EventRelationship.Before relationship) =>
            relationship.BeforeEvent.EventDate.Copy.SubtractOffset(relationship.DateTimeOffset);
    }
}
