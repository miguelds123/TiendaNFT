using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record TipoTarjetaDTO
    {

        public int IdTipoTarjeta { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Descripción")]

        public string Descrpcion { get; set; } = null!;
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

    }
}
