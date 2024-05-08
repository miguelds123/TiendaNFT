using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class TipoTarjeta
{
    public int IdTipoTarjeta { get; set; }

    public string Descrpcion { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; } = new List<FacturaEncabezado>();
}
