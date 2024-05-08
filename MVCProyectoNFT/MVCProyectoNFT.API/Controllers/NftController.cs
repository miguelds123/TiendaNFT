using Microsoft.AspNetCore.Mvc;
using MVCProyectoNFT.Application.DTOs;
using MVCProyectoNFT.Application.Services.Interfaces;

namespace MVCProyectoNFT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NftController : Controller
    {
        private readonly IServiceNft _serviceNft;
        private readonly IServiceClienteNFT _serviceClienteNFT;
        public NftController(IServiceNft servicioNft, IServiceClienteNFT serviceClienteNFT)
        {
            _serviceNft = servicioNft;
            _serviceClienteNFT = serviceClienteNFT;
        }

        [HttpGet("nft")]
        public async Task<IActionResult> GetNft()
        {
            var collection = await _serviceNft.ListAsync();
            return Ok(collection);
        }

        [HttpGet("nft/{id}")]
        public async Task<IActionResult> GetNftById(string id)
        {
            var @object = await _serviceNft.FindByIdAsync(id);

            if (@object != null)
                return Ok(@object);
            else
                return NotFound($"No existe {id}");

        }

        [HttpGet("nft/descripcion/{descripcion}")]
        public async Task<IActionResult> GetProductoByDescription(string descripcion)
        {
            var resultado = new
            {
                Nft = await _serviceNft.FindByDescriptionAsync(descripcion),
                Cliente = await _serviceClienteNFT.FindByNombreNFTAsync(descripcion)
            };

            if (resultado.Cliente != null && resultado.Nft != null)
                return Ok(resultado);
            else
                return NotFound($"No existe {descripcion} o no posee dueño actualmente");
        }


        [HttpPost("nft/create")]
        public async Task<IActionResult> create(NftDTO dto)
        {
            _ = await _serviceNft.AddAsync(dto);
            return Ok();
        }
    }
}
