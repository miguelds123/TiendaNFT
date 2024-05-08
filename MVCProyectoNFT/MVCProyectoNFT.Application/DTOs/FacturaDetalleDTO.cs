using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record FacturaDetalleDTO
    {
        [Display(Name = "No Factura")]
        public int IdFactura { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Detalle")]
        public int IdDetalle { get; set; }
        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "NFT")]
        public string? IdNft { get; set; }

        [Display(Name = "Nombre NFT")]
        public string NombreNft { get; set; } = default!;

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "{0} es requerido")]
        [Display(Name = "Precio")]
        public decimal? Precio { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = "Total")]
        public decimal TotalLinea { get; set; }
    }
}
