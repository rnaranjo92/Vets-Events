using VetsEvents.Models;
using VetsEvents.Repository;

namespace VetsEvents.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IEventRepository Event { get; private set; }
        public IFollowingRepository Follow { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IEventTypeRepository EventType { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Event = new EventRepository(context);
            Follow = new FollowingRepository(context);
            Attendance = new AttendanceRepository(context);
            EventType = new EventTypeRepository(context);
        }
        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}