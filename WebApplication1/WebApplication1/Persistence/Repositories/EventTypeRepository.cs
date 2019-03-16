using System.Collections.Generic;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public class EventTypeRepository : IEventTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public EventTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<EventType> GetAllEventType()
        {
            return _context.EventTypes.ToList();
        }
    }
}