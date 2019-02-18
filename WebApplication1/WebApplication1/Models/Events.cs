using System;
using System.ComponentModel.DataAnnotations;

namespace VetsEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser EventOrganizer { get; set; }

        [Required]
        public EventType EventType { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public DateTime DateTime { get; set; }
    }
}