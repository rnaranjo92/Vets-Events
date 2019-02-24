using System;
using System.ComponentModel.DataAnnotations;

namespace VetsEvents.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }

        [Required]
        public Event Event { get; private set; }

        protected Notification()
        {

        }

        public Notification(Event @event,NotificationType type)
        {
            if (@event == null)
                throw new ArgumentNullException("@event");

            DateTime = DateTime.Now;
            Event = @event;
            Type = type;
        }

        public static Notification EventCreated(Event @event)
        {
            return new Notification(@event, NotificationType.EventCreated);
        }

        public static Notification EventUpdated(Event @event,DateTime dateTime, string Venue)
        {
            var notification = new Notification(@event, NotificationType.EventUpdated);
            notification.OriginalDateTime = dateTime;
            notification.OriginalVenue = Venue;

            return notification;
        }

        public static Notification EventCanceled(Event @event)
        {
            return new Notification(@event, NotificationType.EventCanceled);
        }

    }
}