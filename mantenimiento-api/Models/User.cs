using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class User
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;
    public string Email { get; set; }
    public bool Active { get; set; }
    public byte[] Salt { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Observation> Observations { get; } = new List<Observation>();

    public virtual ICollection<WorkOrder> WorkOrderIdUserAsignedNavigations { get; } = new List<WorkOrder>();

    public virtual ICollection<WorkOrder> WorkOrderIdUserCreatorNavigations { get; } = new List<WorkOrder>();
}
