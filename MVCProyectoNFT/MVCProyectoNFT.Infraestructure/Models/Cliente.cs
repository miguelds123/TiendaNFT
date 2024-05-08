using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public DateTime FechaN { get; set; }

    public int IdPais { get; set; }

    public bool Estado { get; set; }

    public string Cedula { get; set; } = null!;

    public virtual ICollection<ClienteNft> ClienteNft { get; set; } = new List<ClienteNft>();

    public virtual ICollection<FacturaEncabezado> FacturaEncabezado { get; set; } = new List<FacturaEncabezado>();

    public virtual Pais IdPaisNavigation { get; set; } = null!;
}
