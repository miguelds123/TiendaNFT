using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using X.PagedList;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, procesos")]
    public class PaisController : Controller
    {
        private readonly IServicePais _servicePais;

        public PaisController(IServicePais servicePais)
        {
            _servicePais = servicePais;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var collection = await _servicePais.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: PaisController/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: PaisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaisDTO dto)
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

            await _servicePais.AddAsync(dto);


            return RedirectToAction("Index");

        }


        // GET: PaisController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var @object = await _servicePais.FindByIdAsync(id);

            return View(@object);
        }

        // GET: PaisController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var @object = await _servicePais.FindByIdAsync(id);

            return View(@object);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaisDTO dto)
        {

            await _servicePais.UpdateAsync(id, dto);

            return RedirectToAction("Index");

        }

        // GET: PaisController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _servicePais.FindByIdAsync(id);

            return View(@object);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _servicePais.DeleteAsync(id);

            return RedirectToAction("Index");
        }

    }
}
