using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public interface IEventTypeRepository
    {
        IEnumerable<EventType> GetAllEventType();
    }
}