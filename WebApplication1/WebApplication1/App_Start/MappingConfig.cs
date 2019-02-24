using AutoMapper;
using VetsEvents.Dtos;
using VetsEvents.Models;

namespace VetsEvents.App_Start
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<Event, EventDto>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}