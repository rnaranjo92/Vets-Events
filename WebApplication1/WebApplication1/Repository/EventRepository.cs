using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public class EventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Event GetEventWithItsAttendees(int eventId)
        {
            return _context.Events
                .Include(e => e.Attendances.Select(a => a.Attendee))
                .Single(e => e.Id == eventId);
        }


        public IEnumerable<Event> GetAllUserAttending(string userId)
        {
            return _context.Attendance
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(a => a.EventOrganizer)
                .Include(a => a.EventType)
                .ToList();
        }
    }
}