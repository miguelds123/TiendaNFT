using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record NftDTO
    {
        
        public string Id { get; set; } = default!;

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Autor { get; set; } = null!;

        [Display(Name = "Valor en dolares")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string Valor { get; set; } = null!;

        [Display(Name = "Imagen")]
        [Required(ErrorMessage = "{0} es requerido")]
        public byte[] Imagen { get; set; } = null!;
    }
}
