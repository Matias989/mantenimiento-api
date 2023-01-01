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
            .ForMember(d => d.IdUserAsigned,
                 opt => opt.MapFrom(s => s.IdUserAsigned))
            .ForMember(d => d.IdUserCreator,
                 opt => opt.MapFrom(s => s.IdUserCreator))
            .ReverseMap();
        }
    }
}
