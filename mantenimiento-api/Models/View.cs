using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class View
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<ViewsPermission> ViewsPermissions { get; } = new List<ViewsPermission>();
}
