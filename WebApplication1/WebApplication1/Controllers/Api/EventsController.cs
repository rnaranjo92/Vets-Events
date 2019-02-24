using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using VetsEvents.Models;

namespace VetsEvents.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var vetEvents = _context.Events.Single(e => e.Id == id && e.EventOrganizerId == userId);

            if (vetEvents.IsCanceled)
                return NotFound();

            var notification = new Notification
            {
                Event = vetEvents,
                Type = NotificationType.EventCanceled,
                DateTime = DateTime.Now,
            };

            var attendees = _context.Attendance
                .Where(a => a.EventId == vetEvents.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach(var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }

            vetEvents.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
