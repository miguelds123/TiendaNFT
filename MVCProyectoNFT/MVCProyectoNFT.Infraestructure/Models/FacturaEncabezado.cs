using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class FacturaEncabezado
{
    public int Id { get; set; }

    public int? IdTipoTarjeta { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? Fecha { get; set; }

    public bool? Estado { get; set; }

    public string? NumeroTarjeta { get; set; }

    public virtual ICollection<ClienteNft> ClienteNft { get; set; } = new List<ClienteNft>();

    public virtual ICollection<FacturaDetalle> FacturaDetalle { get; set; } = new List<FacturaDetalle>();

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual TipoTarjeta? IdTipoTarjetaNavigation { get; set; }
}
