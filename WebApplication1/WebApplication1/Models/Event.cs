using System;


namespace VetsEvents.Models
{
    public class Event
    {
        public int Id { get; set; }
        public ApplicationUser Organizer { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public Genre Genre { get; set; }
    }
}