using System;
using System.Collections.Generic;

namespace DL;

public partial class EstatusEntrega
{
    public int IdEstatus { get; set; }

    public string Estatus { get; set; } = null!;

    public virtual ICollection<Entrega> Entregas { get; set; } = new List<Entrega>();
}
