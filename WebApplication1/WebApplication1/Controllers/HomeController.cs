using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VetsEvents.Models;
using VetsEvents.ViewModels;

namespace VetsEvents.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upcomingEvents = _context.Events
                .Include(c => c.EventOrganizer)
                .Include(c=>c.EventType)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(g =>
                    g.EventOrganizer.Name.Contains(query) ||
                    g.EventType.Name.Contains(query) ||
                    g.Venue.Contains(query));
            }
            var userId = User.Identity.GetUserId();

            var attendances = _context.Attendance
                .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.EventId);

            

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Title = "Upcoming events",
                SearchTerm = query,
                Attendances = attendances,
            };


            return View("Events",viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}