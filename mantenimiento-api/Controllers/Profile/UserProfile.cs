using mantenimiento_api.Controllers.VM;
using mantenimiento_api.Models;
using AutoMapper;
using mantenimiento_api.Controllers.RR.VM;

namespace mantenimiento_api.Controllers.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserVM>()
            .ForMember(d => d.Email,opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Name,opt => opt.MapFrom(s => s.Name))
            .ForMember(d => d.Active, opt => opt.MapFrom(s => s.Active))
            .ReverseMap();
        }

    }
}
