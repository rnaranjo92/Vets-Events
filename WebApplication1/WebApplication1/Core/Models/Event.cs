using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace VetsEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser EventOrganizer { get; set; }

        public string EventOrganizerId { get; set; }

        public EventType EventType { get; set; }

        public byte EventTypeId { get;  set; }

        public string Venue { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        
        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.EventCanceled(this);


            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Update(DateTime dateTime, string venue, byte eventType)
        {
            var notification = Notification.EventUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            EventTypeId = eventType;

            foreach(var attendee in Attendances.Select(a=>a.Attendee))
                attendee.Notify(notification);
        }

    }
}