using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class EventsViewModel
    {
        public IEnumerable<Event> UpcomingEvents { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Title { get; set; }
        public string SearchTerm { get; set; }
    }
}