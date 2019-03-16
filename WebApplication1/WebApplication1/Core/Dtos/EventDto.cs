using System;

namespace VetsEvents.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }
        public bool IsCanceled { get; private set; }
        public UserDto EventOrganizer { get; set; }
        public EventTypeDto EventType { get; private set; }
        public string Venue { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}