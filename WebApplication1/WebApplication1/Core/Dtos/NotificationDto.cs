using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VetsEvents.Models;

namespace VetsEvents.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string OriginalVenue { get; private set; }
        public EventDto Event { get; private set; }
    }
}