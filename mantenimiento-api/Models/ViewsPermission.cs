using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class ViewsPermission
{
    public int Id { get; set; }

    public int IdPermission { get; set; }

    public int IdView { get; set; }

    public virtual Permission IdPermissionNavigation { get; set; } = null!;

    public virtual View IdViewNavigation { get; set; } = null!;

    public virtual ICollection<RolsViewsPermission> RolsViewsPermissions { get; } = new List<RolsViewsPermission>();
}
