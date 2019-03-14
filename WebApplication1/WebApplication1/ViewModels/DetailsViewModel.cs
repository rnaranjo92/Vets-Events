using System;
using System.Linq;
using VetsEvents.Models;

namespace VetsEvents.ViewModels
{
    public class DetailsViewModel
    {
        public ApplicationUser EventOrganizer { get; set; }
        public string Venue { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsFollowing { get; set; }
        public bool IsGoing { get; set; }
        public ILookup<string, Following> Followings { get; internal set; }
        public bool IsAuthenticated { get; set; }
    }
}