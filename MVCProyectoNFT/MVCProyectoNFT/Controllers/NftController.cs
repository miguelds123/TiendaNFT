using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;
using X.PagedList;

namespace MVCProyectoNFT.Web.Controllers
{
    [Authorize(Roles = "admin, procesos")]
    public class NftController : Controller
    {
        private readonly IServiceNft _serviceNft;

        public NftController(IServiceNft serviceNft)
        {
            _serviceNft = serviceNft;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? page)
        {
            var collection = await _serviceNft.ListAsync();
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
        public async Task<IActionResult> Create(NftDTO dto, IFormFile imageFile)
        {
            MemoryStream target = new MemoryStream();

            // Cuando es Insert Image viene en null porque se pasa diferente
            if (dto.Imagen == null)
            {
                if (imageFile != null)
                {
                    imageFile.OpenReadStream().CopyTo(target);

                    dto.Imagen = target.ToArray();
                    ModelState.Remove("Imagen");
                }
            }

            Guid id = Guid.NewGuid();

            string idString = id.ToString();

            dto.Id = idString;

            ModelState.Remove("Id");

            if (!ModelState.IsValid)
            {
                // Lee del ModelState todos los errores que
                // vienen para el lado del server
                string errors = string.Join("; ", ModelState.Values
                                   .SelectMany(x => x.Errors)
                                   .Select(x => x.ErrorMessage));
                // Response errores
                return BadRequest(errors);
            }

            await _serviceNft.AddAsync(dto);
            return RedirectToAction("Index");
        }
        // GET: PaisController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var @object = await _serviceNft.FindByIdAsync(id);

            return View(@object);
        }

        // GET: PaisController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var @object = await _serviceNft.FindByIdAsync(id);

            return View(@object);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, NftDTO dto, IFormFile imageFile)
        {

            MemoryStream target = new MemoryStream();

            // Cuando es Insert Image viene en null porque se pasa diferente
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    dto.Imagen = memoryStream.ToArray();
                }
            }
            else
            {
                NftDTO nft = await _serviceNft.FindByIdAsync(id);
                dto.Imagen = nft.Imagen;
            }


            await _serviceNft.UpdateAsync(id, dto);

            return RedirectToAction("Index");

        }

        // GET: PaisController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var @object = await _serviceNft.FindByIdAsync(id);

            return View(@object);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IFormCollection collection)
        {
            await _serviceNft.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetNftByName(string filtro)
        {

            var collection = await _serviceNft.FindByDescriptionAsync(filtro);
            return Json(collection);

        }
    }
}
