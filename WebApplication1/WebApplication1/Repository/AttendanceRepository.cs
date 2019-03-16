using System;
using System.Collections.Generic;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Attendance> GetFutureAttendees(string userId)
        {
            return _context.Attendance
               .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
               .ToList();
        }

        public bool CheckIfAttending(Event @event, string userId)
        {
            return _context.Attendance
                    .Any(a => a.EventId == @event.Id && a.AttendeeId == userId);
        }
    }
}