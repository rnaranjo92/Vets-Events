using System.Collections.Generic;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUserIamFollowing(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToList();
        }
        public bool CheckIfFollowing(Event @event,string userId)
        {
            return _context.Followings
                    .Any(f => f.FolloweeId == @event.EventOrganizerId && f.FollowerId == userId);
        }
        public IEnumerable<Following> GetAllFollowings()
        {
            return _context.Followings.ToList();
        }
        

    }
}