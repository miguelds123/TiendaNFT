using System;
using System.Collections.Generic;

namespace MVCProyectoNFT.Infraestructure.Models;

public partial class Usuario
{
    public string NombreUsuario { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido1 { get; set; } = null!;

    public string Apellido2 { get; set; } = null!;

    public bool Estado { get; set; }

    public string Contrasenna { get; set; } = null!;

    public int IdTipoUsuario { get; set; }

    public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; } = null!;
}
