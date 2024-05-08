using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Implementations;
using MVCProyectoNFT.Application.Services.Interfaces;
using X.PagedList;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, procesos")]
    public class TipoTarjetaController : Controller
    {
        private readonly IServiceTipoTarjeta _serviceTipoTarjeta;

        public TipoTarjetaController(IServiceTipoTarjeta serviceTipoTarjeta)
        {
            _serviceTipoTarjeta = serviceTipoTarjeta;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var collection = await _serviceTipoTarjeta.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: TipoTarjetaController/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: TipoTarjetaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoTarjetaDTO dto)
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

            await _serviceTipoTarjeta.AddAsync(dto);


            return RedirectToAction("Index");

        }


        // GET: TipoTarjetaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceTipoTarjeta.FindByIdAsync(id);

            return View(@object);
        }

        // GET: TipoTarjetaController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var @object = await _serviceTipoTarjeta.FindByIdAsync(id);

            return View(@object);
        }

        // POST: TipoTarjetaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoTarjetaDTO dto)
        {

            await _serviceTipoTarjeta.UpdateAsync(id, dto);

            return RedirectToAction("Index");

        }

        // GET: TipoTarjetaController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceTipoTarjeta.FindByIdAsync(id);

            return View(@object);
        }

        // POST: TipoTarjetaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceTipoTarjeta.DeleteAsync(id);

            return RedirectToAction("Index");
        }

    }
}
