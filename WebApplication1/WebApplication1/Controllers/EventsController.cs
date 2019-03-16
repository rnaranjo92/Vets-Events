using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using VetsEvents.Models;
using VetsEvents.Persistence;
using VetsEvents.ViewModels;

namespace VetsEvents.Controllers
{
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [Authorize]
        public ActionResult Mine()
        {
            var events = _unitOfWork.Event.GetAllMyEvent(User.Identity.GetUserId());

            return View(events);
        }

        [Authorize]
        public ActionResult Following()
        {
            var viewModel = new FollowViewModel
            {
                FolloweeId = _unitOfWork.Follow.GetAllUserIamFollowing(User.Identity.GetUserId())
            };

            return View(viewModel);
        }

       

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new EventsViewModel
            {
                Attendances = _unitOfWork.Attendance.GetFutureAttendees(userId).ToLookup(a => a.EventId),
                UpcomingEvents = _unitOfWork.Event.GetAllUserAttending(userId),
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
                EventTypes = _unitOfWork.EventType.GetAllEventType(),
                Title = "Add an Event"
            };
            return View("EventForm",viewModel);
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var vetEvents = _unitOfWork.Event.GetMyEvent(id, User.Identity.GetUserId());

            if (vetEvents == null)
                return HttpNotFound();

            if (vetEvents.EventOrganizerId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new EventFormViewModel
            {
                Id = vetEvents.Id,
                EventTypes = _unitOfWork.EventType.GetAllEventType(),
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
                viewModel.EventTypes = _unitOfWork.EventType.GetAllEventType();
                return View("EventForm", viewModel);
            }

            var VetEvent = new Event
            {
                EventOrganizerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                EventTypeId = viewModel.EventType,
                Venue = viewModel.Venue
            };
            _unitOfWork.Event.Add(VetEvent);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.EventTypes = _unitOfWork.EventType.GetAllEventType();
                return View("EventForm", viewModel);
            }

            var VetEvent = _unitOfWork.Event.GetEventWithItsAttendees(viewModel.Id);

            if (VetEvent == null)
                throw new NullReferenceException();

            if (VetEvent.EventOrganizerId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            VetEvent.Update(viewModel.GetDateTime(), viewModel.Venue, viewModel.EventType);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Events");
        }

        public ActionResult Details(int id)
        {
            var @event = _unitOfWork.Event.GetEventDetails(id);

            if (@event == null)
                return HttpNotFound();

            var followings = _unitOfWork.Follow.GetAllFollowings().ToLookup(f => f.FolloweeId);


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

                viewModel.IsFollowing =
                    _unitOfWork.Follow.CheckIfFollowing(@event, userId) !=null;

                viewModel.IsGoing =
                    _unitOfWork.Attendance.CheckIfAttending(@event, @event.EventOrganizerId) != null;

                viewModel.IsAuthenticated = true;
            }

            return View("Details",viewModel);
        }
    }
}