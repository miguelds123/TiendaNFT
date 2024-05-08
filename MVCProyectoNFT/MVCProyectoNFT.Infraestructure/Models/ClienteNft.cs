using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class ClienteNft
{
    public string IdNft { get; set; } = null!;

    public int IdCliente { get; set; }

    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Estado { get; set; }

    public int? IdFactura { get; set; }

    public string? NombreNft { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual FacturaEncabezado? IdFacturaNavigation { get; set; }

    public virtual Nft IdNftNavigation { get; set; } = null!;
}
