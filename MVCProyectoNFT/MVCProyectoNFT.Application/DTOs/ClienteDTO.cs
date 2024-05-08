using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public class ClienteDTO
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Apellido")]
        public string Apellido1 { get; set; } = null!;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Apellido")]
        public string Apellido2 { get; set; } = null!;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; } = null!;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; } = null!;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaN { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "ID Pais")]
        public int IdPais { get; set; }

        [Display(Name = "Pais")]
        public string? DescripcionPais { get; set; } = default;

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; } = null!;
    }
}
