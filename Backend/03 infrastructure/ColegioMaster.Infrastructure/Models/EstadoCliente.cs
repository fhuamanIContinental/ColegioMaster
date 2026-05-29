using System;
using System.Collections.Generic;

namespace ColegioMaster.Infrastructure.Models;

public partial class EstadoCliente
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
