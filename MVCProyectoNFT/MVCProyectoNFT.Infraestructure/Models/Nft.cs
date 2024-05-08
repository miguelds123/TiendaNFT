using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class Nft
{
    public string Id { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public decimal Valor { get; set; }

    public int Cantidad { get; set; }

    public byte[] Imagen { get; set; } = null!;

    public virtual ICollection<ClienteNft> ClienteNft { get; set; } = new List<ClienteNft>();

    public virtual ICollection<FacturaDetalle> FacturaDetalle { get; set; } = new List<FacturaDetalle>();
}
