using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class EventFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        [Display(Name ="Type of Event")]
        public byte EventType { get; set; }

        public IEnumerable<EventType> EventTypes { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time)); 
        }
    }

}