using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using VetsEvents.Dtos;
using VetsEvents.Models;

namespace VetsEvents.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following already exists");

            var following = new Following()
            {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var followings = _context.Followings.SingleOrDefault(f=>f.FolloweeId == id && f.FollowerId == userId);

            if (followings == null)
                return BadRequest();

            _context.Followings.Remove(followings);
            _context.SaveChanges();

            return Ok(id);
        }

    }
}
