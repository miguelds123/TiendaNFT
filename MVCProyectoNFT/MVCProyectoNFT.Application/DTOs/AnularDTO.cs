using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProyectoNFT.Application.DTOs
{
    public record AnularDTO
    {
        [Display(Name = "Id de la factura que desea anular")]
        [Required(ErrorMessage = "{0} es requerido")]
        public int Id { get; set; }
    }
}
