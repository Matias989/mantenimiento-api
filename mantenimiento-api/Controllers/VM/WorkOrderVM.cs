using mantenimiento_api.Controllers.VM;
using mantenimiento_api.Models;

namespace mantenimiento_api.Controllers.RR.VM
{
    public class WorkOrderVM
    {
        public int Id { get; set; }
        public UserVM UserCreator { get; set; }
        public UserVM? UserAsigned { get; set; }
        public int Progress { get; set; }
        public string Observation { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Priority { get; set; }
        public List<Observation> Observations { get; } = new List<Observation>();
        public List<Picture> Pictures { get; } = new List<Picture>();
    }
}
