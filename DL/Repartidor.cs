using System;
using System.Collections.Generic;

namespace DL;

public partial class Repartidor
{
    public int IdRepartidor { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public int IdUnidad { get; set; }

    public string Telefono { get; set; } = null!;

    public DateTime FechaIngreso { get; set; }

    public string Fotografia { get; set; } = null!;

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();

    public virtual UnidadEntrega IdUnidadNavigation { get; set; } = null!;
}
