using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using VetsEvents.Models;
using VetsEvents.Models.Dto;

namespace VetsEvents.Controllers.Api
{

    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var currentUser = User.Identity.GetUserId();

            if (_context.Attendance.Any(a => a.AttendeeId == currentUser && a.EventId == dto.EventId))
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                EventId = dto.EventId,
                AttendeeId = currentUser
            };
            _context.Attendance.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendance.SingleOrDefault(a => a.AttendeeId == userId && a.EventId == id);
            if (attendance == null)
                return BadRequest("Attendance does not exits");

            _context.Attendance.Remove(attendance);
            _context.SaveChanges();


            return Ok(id);
        }
    }
}
