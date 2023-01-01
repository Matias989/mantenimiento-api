using mantenimiento_api.Controllers.VM;
using mantenimiento_api.Models;

namespace mantenimiento_api.Controllers.RR.VM
{
    public class WorkOrderVM
    {
        public int Id { get; set; }
        public int IdUserCreator { get; set; }
        public int? IdUserAsigned { get; set; }
        public int Progress { get; set; }
        public string Observation { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public int? Priority { get; set; }
        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }
}
