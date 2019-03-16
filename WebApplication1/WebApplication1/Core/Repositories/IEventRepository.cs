using System.Collections.Generic;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public interface IEventRepository
    {
        IQueryable<Event> GetAllUpcomingEvent();
        Event GetEventWithItsAttendees(int eventId);
        IQueryable<Event> GetEventBySearch(IQueryable<Event> upcomingEvents, string query);
        IEnumerable<Event> GetAllUserAttending(string userId);
        IEnumerable<Event> GetAllMyEvent(string userId);
        Event GetMyEvent(int id, string userId);
        Event GetEventDetails(int id);
        void Add(Event @event);

    }
}