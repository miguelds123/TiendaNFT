using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.Services.Interfaces;
using MVCProyectoNFT.Infraestructure.Models;
using MVCProyectoNFT.Infraestructure.Repository.Interface;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, reportes")]
    public class GraficoController : Controller
    {
        private readonly IServiceNft _serviceNft;
        private readonly IServiceReporte _serviceReporte;
        private readonly IServiceCliente _serviceCliente;
        private readonly IRepositoryClienteNFT _repositoryClienteNFT;
        public GraficoController(IServiceNft serviceNft,
                                 IServiceReporte serviceReporte,
                                 IServiceCliente serviceCliente,
                                 IRepositoryClienteNFT repositoryClienteNFT)
        {
            _serviceNft = serviceNft;
            _serviceReporte = serviceReporte;
            _serviceCliente = serviceCliente;
            _repositoryClienteNFT = repositoryClienteNFT;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GraficoVentas()
        {
            return View();
        }

        public async Task<IActionResult> VentasXCliente(DateTime fechaInicio, DateTime fechaFin)
        {
            string etiquetas = "";
            string precios = "";
            decimal total = 0M;

            //if (string.IsNullOrEmpty(idCliente))
            //{
            //    ViewBag.Message = $"Cliente Id requerido";
            //    return View("GraficoVentas");
            //}

            var clienteNft = await _repositoryClienteNFT.ListAsync(fechaInicio, fechaFin.AddDays(1));

            if (clienteNft == null)
            {
                ViewBag.Message = $"ClienteNft No existe";
                return View("GraficoVentas");
            }

            //var lista = await _serviceReporte.BillsByClientIdAsync(idCliente);

            //if (lista.Count == 0)
            //{
            //    ViewBag.Message = $"No hay reportes de ventas para Id = {idCliente}";
            //    return View("GraficoVentas");
            //}

            foreach (var item in clienteNft)
            {
                // concatenade 
                etiquetas += "" + item.IdFactura + ",";
                // Has List Any data?
                var sigma = item.IdNftNavigation.Valor;
                total += sigma;
                precios += sigma.ToString("##") + ",";
            }
            etiquetas = etiquetas.Substring(0, etiquetas.Length - 1); // ultima coma
            precios = precios.Substring(0, precios.Length - 1);

            ViewBag.GraphTitle = $"Ventas del {fechaInicio} al {fechaFin} -  Total de Ventas {total.ToString("###,###.00")} ";
            ViewBag.Etiquetas = etiquetas;
            ViewBag.Valores = precios;
            return View("GraficoVentas");
        }
    }
}
