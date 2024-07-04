using CarrinhoDeCompras.Data;
using CarrinhoDeCompras.Interfaces.Services;
using CarrinhoDeCompras.Models.Requests;
using CarrinhoDeCompras.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CarrinhoDeCompras.Controllers
{
    [Route("api/carrinho")]
    [ApiController]
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly AppDbContext _db;

        public CarrinhoController(ICarrinhoService carrinhoService, AppDbContext db)
        {
            _carrinhoService = carrinhoService;
            _db = db;
        }

        [HttpGet]
        [Route("GetCarrinho")]
        public async Task<IActionResult> GetCarrinho()
        {
            var carrinho = await _carrinhoService.GetCarrinhoAsync();
            if (carrinho == null)
            {
                return NotFound();
            }
            return View("GetCarrinho", carrinho);
        }

        [HttpGet]
        [Route("CreateItemCarrinho")]
        public IActionResult CreateItemCarrinho()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateItemCarrinho")]
        public async Task<ActionResult<CreateItemResponse>> CreateItemCarrinho([FromForm] CreateItemRequest itemCarrinho)
        {
            if (!ModelState.IsValid)
            {
                return View(itemCarrinho);
            }

            var itemResponse = await _carrinhoService.CreateItemCarrinhoAsync(itemCarrinho);
            TempData["MensagemSucesso"] = "Item adicionado com sucesso!";
            return RedirectToAction(nameof(CreateItemCarrinho));
        }

        [HttpDelete("{id}")]
        [Route("RemoveItem/{id}")]
        public async Task<IActionResult> RemoveItem(int id)
        {
            await _carrinhoService.RemoveItemAsync(id);
            TempData["MensagemSucesso"] = "Item removido com sucesso!";
            return RedirectToAction(nameof(GetCarrinho));
        }

        [HttpGet]
        [Route("UpdateItemCarrinho")]
        public IActionResult UpdateItemCarrinho()
        {
            return View();
        }

        [HttpPut("{id}")]
        [Route("UpdateItemCarrinho")]
        public async Task<IActionResult> UpdateItemCarrinho([FromForm] UpdateItemRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _carrinhoService.UpdateItemCarrinhoAsync(request);
            TempData["MensagemSucesso"] = "Item atualizado com sucesso!";
            return RedirectToAction(nameof(GetCarrinho));
        }
    }
}

