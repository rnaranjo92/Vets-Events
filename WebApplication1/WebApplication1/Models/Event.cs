using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VetsEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser EventOrganizer { get; set; }

        [Required]
        public string EventOrganizerId { get; private set; }

        public EventType EventType { get; private set; }

        [Required]
        public byte EventTypeId { get; private set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; private set; }

        [Required]
        public DateTime DateTime { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Event()
        {
            Attendances = new Collection<Attendance>();
        }

        public Event(string eventOrganizerId,DateTime dateTime, string venue, byte eventTypeId)
        {
            if (eventOrganizerId == null)
                throw new ArgumentNullException();

            EventOrganizerId = eventOrganizerId;
            DateTime = dateTime;
            Venue = venue;
            EventTypeId = eventTypeId;
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
            var notification = Notification.EventUpdated(this, dateTime, venue);

            Venue = venue;
            DateTime = dateTime;
            EventTypeId = eventType;


            foreach(var attendee in Attendances.Select(a=>a.Attendee))
                attendee.Notify(notification);
        }

    }
}