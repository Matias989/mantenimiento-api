using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<RolsViewsPermission> RolsViewsPermissions { get; } = new List<RolsViewsPermission>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
