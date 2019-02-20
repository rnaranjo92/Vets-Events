using Microsoft.AspNet.Identity;
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