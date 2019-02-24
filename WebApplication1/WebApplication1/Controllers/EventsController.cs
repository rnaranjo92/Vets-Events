using Microsoft.AspNet.Identity;
using System;
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
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Events
                .Where(e => e.EventOrganizerId == userId && e.DateTime > DateTime.Now && !e.IsCanceled)
                .Include(e=>e.EventType)
                .ToList();


            return View(events);
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
                EventTypes = _context.EventTypes.ToList(),
                Title = "Add an Event"
            };
            return View("EventForm",viewModel);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var vetEvents = _context.Events.Single(e => e.Id == id && e.EventOrganizerId == userId);
            var viewModel = new EventFormViewModel
            {
                Id = vetEvents.Id,
                EventTypes = _context.EventTypes.ToList(),
                EventType = vetEvents.EventTypeId,
                Venue = vetEvents.Venue,
                Date = vetEvents.DateTime.ToString("d MMM yyyy"),
                Time = vetEvents.DateTime.ToString("HH:mm"),
                Title = "Edit" ,
            };
            return View("EventForm",viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EventTypes = _context.EventTypes.ToList();
                return View("EventForm", viewModel);
            }

            var VetEvent = new Event
                (User.Identity.GetUserId(),
                viewModel.GetDateTime(),
                viewModel.Venue, 
                viewModel.EventType);
            
            _context.Events.Add(VetEvent);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EventTypes = _context.EventTypes.ToList();
                return View("EventForm", viewModel);
            }
            var userId = User.Identity.GetUserId();

            var VetEvent = _context.Events.Single(e => e.Id == viewModel.Id && e.EventOrganizerId == userId);


            VetEvent.Update(viewModel.GetDateTime(), viewModel.Venue, viewModel.EventType);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }
    }
}