using System;
using System.Collections.Generic;

namespace mantenimiento_api.Models;

public partial class WorkOrder
{
    public int Id { get; set; }

    public int IdUserCreator { get; set; }

    public int? IdUserAsigned { get; set; }

    public int Progress { get; set; }

    public string Observation { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public DateTime? FinishDate { get; set; }

    public int? Priority { get; set; }

    public virtual User? IdUserAsignedNavigation { get; set; }

    public virtual User IdUserCreatorNavigation { get; set; } = null!;

    public virtual ICollection<Observation> Observations { get; } = new List<Observation>();

    public virtual ICollection<Picture> Pictures { get; } = new List<Picture>();
}
