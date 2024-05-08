using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class Pais
{
    public int Id { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Cliente> Cliente { get; set; } = new List<Cliente>();
}
