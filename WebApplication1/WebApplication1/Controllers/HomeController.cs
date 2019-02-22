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
        public ActionResult Index()
        {
            var upcomingEvents = _context.Events
                .Include(c => c.EventOrganizer)
                .Include(c=>c.EventType)
                .Where(g => g.DateTime > DateTime.Now);

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Title = "Upcoming events"
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