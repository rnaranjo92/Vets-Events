using System;
using System.ComponentModel.DataAnnotations;

namespace VetsEvents.Models
{
    public class Event
    {
        public int Id { get; set; }

        public ApplicationUser EventOrganizer { get; set; }

        [Required]
        public string EventOrganizerId { get; set; }

        public EventType EventType { get; set; }

        [Required]
        public byte EventTypeId { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}