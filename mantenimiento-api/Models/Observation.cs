using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class Observation
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdWorkOrder { get; set; }

    public string Msj { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual WorkOrder IdWorkOrderNavigation { get; set; } = null!;
}
