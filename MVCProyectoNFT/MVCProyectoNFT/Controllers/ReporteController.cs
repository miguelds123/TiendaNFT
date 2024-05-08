using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.Services.Interfaces;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, reportes")]
    public class ReporteController : Controller
    {
        private readonly IServiceReporte _servicioReporte;
        public ReporteController(IServiceReporte servicioReporte)
        {
            _servicioReporte = servicioReporte;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NFTReport()
        {
            return View();
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public async Task<FileResult> NFTReportPDF()
        {

            byte[] bytes = await _servicioReporte.NFTReport();
            return File(bytes, "text/plain", "file.pdf");

        }

        public IActionResult DuenoNFTReport()
        {
            return View();
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public async Task<FileResult> DuenoNFTReportPDF(string nombre)
        {

            byte[] bytes = await _servicioReporte.DuenoNFTReportPDF(nombre);
            return File(bytes, "text/plain", "file.pdf");

        }

        public IActionResult VentasReport()
        {
            return View();
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public async Task<FileResult> VentasReportPDF(DateTime fechaInicio, DateTime fechaFin)
        {

            byte[] bytes = await _servicioReporte.ListaVentas(fechaInicio.Date, fechaFin.Date.AddDays(1));
            return File(bytes, "text/plain", "file.pdf");

        }

        public IActionResult ClienteReport()
        {
            return View();
        }

        [HttpPost]
        [RequireAntiforgeryToken]
        public async Task<FileResult> ClienteReportPDF()
        {

            byte[] bytes = await _servicioReporte.ClienteReport();
            return File(bytes, "text/plain", "file.pdf");

        }
    }
}
