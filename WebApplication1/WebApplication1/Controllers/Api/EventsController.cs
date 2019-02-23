using Microsoft.AspNet.Identity;
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

            vetEvents.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
