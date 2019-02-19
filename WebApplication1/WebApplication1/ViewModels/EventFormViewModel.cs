using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class EventFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public byte EventType { get; set; }
        public IEnumerable<EventType> EventTypes { get; set; }
    }
}