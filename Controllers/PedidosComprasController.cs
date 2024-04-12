using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessoManyminds_Back.Business.Interfaces;
using ProcessoManyminds_Back.Datas.Repository;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;
using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidosComprasController : Controller
    {
        private readonly IPedidosComprasRepository _pedidosComprasRepository;
        private readonly IPedidosComprasBusiness _pedidosComprasBusiness;

        public PedidosComprasController(IPedidosComprasRepository pedidosComprasRepository, IPedidosComprasBusiness pedidosComprasBusiness)
        {
            _pedidosComprasRepository = pedidosComprasRepository;
            _pedidosComprasBusiness = pedidosComprasBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> PedidosCompras()
        {
            return Ok(await _pedidosComprasRepository.GetPedidosAsync());
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPedido([FromBody] PedidosCompras pedidoCompras)
        {
            await _pedidosComprasBusiness.CalcularValorTotal(pedidoCompras);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditarPedidoCompra([FromBody] PedidosCompras pedidoCompras)
        {
            await _pedidosComprasBusiness.CalcularValorTotal(pedidoCompras, true);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> FinalizarPedidoCompra(string id)
        {
            var pedido = await _pedidosComprasRepository.GetPedidosByIdAsync(id);
            pedido.Status = 0;
            await _pedidosComprasRepository.UpdatePedidoAsync(pedido);
            return Ok();
        }
    }
}
