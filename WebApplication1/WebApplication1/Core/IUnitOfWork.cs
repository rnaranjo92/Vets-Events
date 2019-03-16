using VetsEvents.Repository;

namespace VetsEvents.Persistence
{
    public interface IUnitOfWork
    {
        IEventRepository Event { get; }
        IFollowingRepository Follow { get; }
        IAttendanceRepository Attendance { get; }
        IEventTypeRepository EventType { get;  }
        void Complete();
    }
}