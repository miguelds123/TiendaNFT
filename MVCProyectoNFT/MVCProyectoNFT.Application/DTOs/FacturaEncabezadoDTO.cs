using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record FacturaEncabezadoDTO
    {
        [Display(Name = "Id")]

        public int Id { get; set; }

        [Display(Name = "Tarjeta")]
        [Required(ErrorMessage = "{0} es requerido")]


        public int IdTipoTarjeta { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Cliente")]

        public int IdCliente { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Fecha")]

        public DateTime? Fecha { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Estado")]

        public bool Estado { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "No Tarjeta")]

        public string NumeroTarjeta { get; set; }

        public List<FacturaDetalleDTO> ListFacturaDetalle = null!;
    }
}
