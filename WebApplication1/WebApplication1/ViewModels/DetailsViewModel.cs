using System;

namespace VetsEvents.ViewModels
{
    public class DetailsViewModel
    {
        public string EventOrganizer { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsGoing { get; set; }
    }
}