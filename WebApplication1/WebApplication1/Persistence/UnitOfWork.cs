using VetsEvents.Models;
using VetsEvents.Repository;

namespace VetsEvents.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public EventRepository Event { get; private set; }
        public FollowingRepository Follow { get; private set; }
        public AttendanceRepository Attendance { get; private set; }
        public EventTypeRepository EventType { get; private set; }
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