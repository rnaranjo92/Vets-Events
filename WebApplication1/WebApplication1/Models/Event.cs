using System;
using System.ComponentModel.DataAnnotations;

namespace VetsEvents.Models
{
    public class Event
    {

        public int Id { get; set; }

        [Required]
        public ApplicationUser Organizer { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}