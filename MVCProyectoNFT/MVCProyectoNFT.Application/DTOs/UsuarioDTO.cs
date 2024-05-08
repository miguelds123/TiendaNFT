using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record class UsuarioDTO
    {
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; } = null!;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "ID Tipo Usuario")]
        public int IdTipoUsuario { get; set; }

        [Display(Name = "Tipo de Usuario")]
        public string? DescripcionRol { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Contraseña")]
        public string? Contrasenna { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Apellido 1")]
        public string? Apellido1 { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Apellido 2")]
        public string? Apellido2 { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        

    }
}
