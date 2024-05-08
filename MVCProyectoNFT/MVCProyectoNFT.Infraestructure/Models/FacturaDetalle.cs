using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class FacturaDetalle
{
    public int IdFactura { get; set; }

    public int IdDetalle { get; set; }

    public string? IdNft { get; set; }

    public decimal? Precio { get; set; }

    public virtual FacturaEncabezado IdFacturaNavigation { get; set; } = null!;

    public virtual Nft? IdNftNavigation { get; set; }
}
