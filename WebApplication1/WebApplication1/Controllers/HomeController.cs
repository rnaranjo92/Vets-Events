using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using VetsEvents.Persistence;
using VetsEvents.ViewModels;

namespace VetsEvents.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index(string query = null)
        {
            var upcomingEvents = _unitOfWork.Event.GetAllUpcomingEvent();

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = _unitOfWork.Event.GetEventBySearch(upcomingEvents, query);
            }
            var userId = User.Identity.GetUserId();

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Title = "Upcoming events",
                SearchTerm = query,
                Attendances = _unitOfWork.Attendance.GetFutureAttendees(userId).ToLookup(a => a.EventId),
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