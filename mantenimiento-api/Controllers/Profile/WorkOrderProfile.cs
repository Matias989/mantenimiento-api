using mantenimiento_api.Controllers.RR.VM;
using mantenimiento_api.Models;
using AutoMapper;
using mantenimiento_api.Controllers.VM;

namespace mantenimiento_api.Controllers.Profile
{
    public class WorkOrderProfile : AutoMapper.Profile
    {
        public WorkOrderProfile()
        {
            CreateMap<User, UserVM>();
            CreateMap<WorkOrder, WorkOrderVM>()
            .ForMember(d => d.UserAsigned,
                 opt => opt.MapFrom(s => s.IdUserAsignedNavigation))
            .ForMember(d => d.UserCreator,
                 opt => opt.MapFrom(s => s.IdUserCreatorNavigation));
        }
    }
}
