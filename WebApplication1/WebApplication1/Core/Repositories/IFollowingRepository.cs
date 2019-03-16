using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.Repository
{
    public interface IFollowingRepository
    {
        IEnumerable<ApplicationUser> GetAllUserIamFollowing(string userId);
        bool CheckIfFollowing(Event @event, string userId);
        IEnumerable<Following> GetAllFollowings();
    }
}