using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class Picture
{
    public int Id { get; set; }

    public int IdWorkOrder { get; set; }

    public string Title { get; set; } = null!;

    public string Path { get; set; } = null!;

    public bool Cover { get; set; }

    public virtual WorkOrder IdWorkOrderNavigation { get; set; } = null!;
}
