using System;

namespace EventBrowser.Domain
{
    /// <summary>
    /// Describes a specific event in time and associates all other information about the event to this instance
    /// </summary>
    public class Event
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Event(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
