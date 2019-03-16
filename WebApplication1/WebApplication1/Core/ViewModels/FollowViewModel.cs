using System.Collections.Generic;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class FollowViewModel
    {
        public IEnumerable<ApplicationUser> FolloweeId { get; set; }
    }
}