using mantenimiento_api.Models;

namespace mantenimiento_api.Controllers.VM
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Active { get; set; }

    }
}
