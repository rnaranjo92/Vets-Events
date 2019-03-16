using System;
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
        public IEnumerable<Event> GetAllMyEvent(string userId)
        {
            return _context.Events
                .Where(e => e.EventOrganizerId == userId && e.DateTime > DateTime.Now && !e.IsCanceled)
                .Include(e => e.EventType)
                .ToList();
        }
        public Event GetMyEvent(int id, string userId)
        {
            return _context.Events
                .Single(e => e.Id == id && e.EventOrganizerId == userId);
        }

        public Event GetEventDetails(int id)
        {
            return _context.Events.Include(e => e.EventOrganizer).Include(e => e.EventType).Single(e => e.Id == id);
        }
        public void Add(Event @event)
        {
            _context.Events.Add(@event);
        }
    }
}