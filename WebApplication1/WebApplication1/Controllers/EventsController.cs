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

            var attendances = _context.Attendance
               .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
               .ToList()
               .ToLookup(a => a.EventId);

            var viewModel = new EventsViewModel
            {
                Attendances = attendances,
                UpcomingEvents = events,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Title = "Events I'm attending"
            };
            return View("Events",viewModel);
        }
        [HttpPost]
        public ActionResult Search(EventsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
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
            {
                EventOrganizerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                EventTypeId = viewModel.EventType,
                Venue = viewModel.Venue
            };

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

            var VetEvent = _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .Single(e => e.Id == viewModel.Id && e.EventOrganizerId == userId);

            VetEvent.Update(viewModel.GetDateTime(), viewModel.Venue, viewModel.EventType);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Events");
        }

        public ActionResult Details(int id)
        {
            var @event = _context.Events.Include(e=>e.EventOrganizer).Include(e=>e.EventType).Single(e => e.Id == id);

            if (@event == null)
                return HttpNotFound();

            var followings = _context.Followings
                .ToList()
                .ToLookup(f => f.FolloweeId);

            var viewModel = new DetailsViewModel
            {
                Venue = @event.Venue,
                DateTime = @event.DateTime,
                EventOrganizer = @event.EventOrganizer,
                Followings = followings,
            };

            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsFollowing  = _context.Followings
                    .Any(f => f.FolloweeId == @event.EventOrganizerId && f.FollowerId == userId);

                viewModel.IsGoing = _context.Attendance
                    .Any(a => a.EventId == @event.Id && a.AttendeeId == userId);

                viewModel.IsAuthenticated = true;
            }

            return View("Details",viewModel);
        }
    }
}