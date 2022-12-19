using mantenimiento_api.Models;

namespace mantenimiento_api.Controllers.VM
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Password { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

    }
}
