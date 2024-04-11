using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;
using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Controllers
{
    [Authorize, Route("api/[controller]"), ApiController]
    public class ProdutosController : Controller
    {
        private readonly IProdutosRepository _produtosRepository;

        public ProdutosController(IProdutosRepository produtosRepository) {
            _produtosRepository = produtosRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProdutos()
        {
            return Ok(await _produtosRepository.GetProdutosAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SalvarProduto([FromBody] Produtos produto)
        {
            await _produtosRepository.SalvarAsync(produto);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditarProduto([FromBody] Produtos produto)
        {
            await _produtosRepository.UpdateAsync(produto);
            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DesativarProduto(int codigo)
        {
            var produto = await _produtosRepository.GetProdutosByCodigoAsync(codigo);
            if(produto != null)
            {
                produto.IsInativo = true;
                await _produtosRepository.UpdateAsync(produto);
            }
            return Ok(produto);
        }
    }
}
