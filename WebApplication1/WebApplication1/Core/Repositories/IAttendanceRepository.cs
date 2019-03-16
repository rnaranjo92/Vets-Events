using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendees(string userId);
        bool CheckIfAttending(Event @event, string userId);
    }
}
