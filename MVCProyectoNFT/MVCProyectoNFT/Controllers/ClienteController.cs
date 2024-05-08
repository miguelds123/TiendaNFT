using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using X.PagedList;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, procesos")]
    public class ClienteController : Controller
    {
        private readonly IServiceCliente _serviceCliente;
        private readonly IServicePais _servicePais;

        public ClienteController(IServiceCliente serviceCliente, IServicePais servicePais)
        {
            _serviceCliente = serviceCliente;
            _servicePais = servicePais;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var collection = await _serviceCliente.ListAsync();
            return View(collection.ToPagedList(page ?? 1, 5));
        }

        // GET: ClienteController/Create
        public async Task<IActionResult> CreateAsync()
        {
            var collection = await _servicePais.ListAsync();
            ViewBag.ListPaises = collection;
            TempData.Keep();
            return View();
        }


        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClienteDTO dto)
        {

            dto.Estado = true;

            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                return BadRequest(errors);
            }

            await _serviceCliente.AddAsync(dto);


            return RedirectToAction("Index");

        }


        // GET: ClienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var @object = await _serviceCliente.FindByIdAsync(id);

            return View(@object);
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var collection = await _servicePais.ListAsync();
            ViewBag.ListPaises = collection;
            TempData.Keep();
            var @object = await _serviceCliente.FindByIdAsync(id);

            return View(@object);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClienteDTO dto)
        {

            await _serviceCliente.UpdateAsync(id, dto);

            return RedirectToAction("Index");

        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var @object = await _serviceCliente.FindByIdAsync(id);

            return View(@object);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await _serviceCliente.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public IActionResult GetClienteByName(string filtro)
        {

            var collections = _serviceCliente.FindByDescriptionAsync(filtro).GetAwaiter().GetResult();

            return Json(collections);
        }

    }
}
