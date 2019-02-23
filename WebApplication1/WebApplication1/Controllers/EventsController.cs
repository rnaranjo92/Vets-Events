using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using VetsEvents.Models;
using VetsEvents.ViewModels;

namespace VetsEvents.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();

            var viewModel = new FollowViewModel
            {
                FolloweeId = following
            };

            return View(viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Attendance
                .Where(a => a.AttendeeId == userId)
                .Select(a=>a.Event)
                .Include(a=>a.EventOrganizer)
                .Include(a =>a.EventType)
                .ToList();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = events,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Title = "Events I'm attending"
            };
            return View("Events",viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                EventTypes = _context.EventTypes.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EventTypes = _context.EventTypes.ToList();
                return View("Create", viewModel);
            }

            var VetEvent = new Event
            {
                EventOrganizerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                EventTypeId = viewModel.EventType,
                Venue = viewModel.Venue
            };
            _context.Events.Add(VetEvent);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        } 
    }
}