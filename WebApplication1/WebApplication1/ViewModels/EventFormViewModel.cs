using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using VetsEvents.Controllers;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class EventFormViewModel
    {
        [Required]
        public string Venue { get; set; }

        public int Id { get; set; }

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

        public string Action {
            get
            {
                Expression<Func<EventsController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<EventsController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;

                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public string Title { get; set; }
    }

}