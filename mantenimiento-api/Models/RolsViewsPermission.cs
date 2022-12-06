using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class RolsViewsPermission
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public int IdViewPermission { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ViewsPermission IdViewPermissionNavigation { get; set; } = null!;
}
