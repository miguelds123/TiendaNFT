using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using System.Text.Json;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, procesos")]
    public class FacturaController : Controller
    {
        private readonly IServiceNft _serviceNft;
        private readonly IServiceTipoTarjeta _serviceTarjeta;
        private readonly IServiceFactura _serviceFactura;
        private readonly IServiceCliente _serviceCliente;

        public FacturaController(IServiceNft servicoNft,
                                 IServiceTipoTarjeta servicoTarjeta,
                                 IServiceFactura serviceFactura,
                                 IServiceCliente serviceCliente)
        {
            _serviceNft = servicoNft;
            _serviceTarjeta = servicoTarjeta;
            _serviceFactura = serviceFactura;
            _serviceCliente = serviceCliente;
        }

        public async Task<IActionResult> Index()
        {

            var nextReceiptNumber = await _serviceFactura.GetNextReceiptNumber();
            ViewBag.CurrentReceipt = nextReceiptNumber;
            var collection = await _serviceTarjeta.ListAsync();
            ViewBag.ListTarjeta = collection;

            // Clear CarShopping
            TempData["CartShopping"] = null;
            TempData.Keep();

            return View();
        }


        public async Task<IActionResult> AddProduct(string id, int cantidad)
        {
            FacturaDetalleDTO facturaDetalleDTO = new FacturaDetalleDTO();
            List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
            string json = "";
            decimal impuesto = 0;

            var Nft = await _serviceNft.FindByIdAsync(id);

            // Stock ??
            //if (cantidad > 0)
            //{
            //    return BadRequest("No hay inventario suficiente!");
            //}

            facturaDetalleDTO.NombreNft = Nft.Nombre;
            facturaDetalleDTO.Cantidad = cantidad;
            facturaDetalleDTO.Precio = Convert.ToDecimal(Nft.Valor);
            facturaDetalleDTO.IdNft = id;
            facturaDetalleDTO.TotalLinea = Convert.ToDecimal(facturaDetalleDTO.Precio);
            if (TempData["CartShopping"] == null)
            {
                lista.Add(facturaDetalleDTO);
                // Reenumerate 
                int idx = 1;
                lista.ForEach(p => p.IdDetalle = idx++);
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }
            else
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
                lista.Add(facturaDetalleDTO);
                // Reenumerate 
                int idx = 1;
                lista.ForEach(p => p.IdDetalle = idx++);
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();
            return PartialView("_DetailFactura", lista);
        }

        public IActionResult GetDetailFactura()
        {
            List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
            string json = "";
            json = (string)TempData["CartShopping"]!;
            lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
            // Reenumerate 
            int idx = 1;
            lista.ForEach(p => p.IdDetalle = idx++);
            json = JsonSerializer.Serialize(lista);
            TempData["CartShopping"] = json;
            TempData.Keep();

            return PartialView("_DetailFactura", lista);
        }

        public IActionResult DeleteProduct(int id)
        {
            FacturaDetalleDTO facturaDetalleDTO = new FacturaDetalleDTO();
            List<FacturaDetalleDTO> lista = new List<FacturaDetalleDTO>();
            string json = "";

            if (TempData["CartShopping"] != null)
            {
                json = (string)TempData["CartShopping"]!;
                lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;
                // Remove from list by Index
                int idx = lista.FindIndex(p => p.IdDetalle == id);
                lista.RemoveAt(idx);
                json = JsonSerializer.Serialize(lista);
                TempData["CartShopping"] = json;
            }

            TempData.Keep();

            // return Content("Ok");
            return PartialView("_DetailFactura", lista);

        }


        public async Task<IActionResult> Create(FacturaEncabezadoDTO facturaEncabezadoDTO)
        {
            string json;
            facturaEncabezadoDTO.Fecha = DateTime.Now;
            try
            {

                // IdClient exist?
                var cliente = await _serviceCliente.FindByIdAsync(facturaEncabezadoDTO.IdCliente);
                if (cliente == null)
                {
                    // Keep Cache data
                    TempData.Keep();
                    return BadRequest("Cliente No Existe!");
                }


                // TODO: Validate! 
                if (!ModelState.IsValid)
                {

                }

                json = (string)TempData["CartShopping"]!;

                if (string.IsNullOrEmpty(json) || (json == "[]"))
                {
                    return BadRequest("No hay datos por facturar");
                }

                var lista = JsonSerializer.Deserialize<List<FacturaDetalleDTO>>(json!)!;

                //Mismo numero de factura para el detalle FK
                lista.ForEach(p => p.IdFactura = facturaEncabezadoDTO.Id);
                facturaEncabezadoDTO.ListFacturaDetalle = lista;
                facturaEncabezadoDTO.Estado = true;

                await _serviceFactura.AddAsync(facturaEncabezadoDTO);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Keep Cache data
                TempData.Keep();
                return BadRequest(ex.Message);
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Anular()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnularFactura(AnularDTO dto)
        {

            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            await _serviceFactura.Anular(dto.Id);


            return RedirectToAction("Anular");
        }
    }
}
