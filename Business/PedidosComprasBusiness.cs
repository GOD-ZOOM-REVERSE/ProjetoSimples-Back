using ProcessoManyminds_Back.Business.Interfaces;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;
using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Business
{
    public class PedidosComprasBusiness : IPedidosComprasBusiness
    {
        private readonly IPedidosComprasRepository _pedidosComprasRepository;

        public PedidosComprasBusiness(IPedidosComprasRepository pedidosComprasRepository)
        {
            _pedidosComprasRepository = pedidosComprasRepository;
        }

        public async Task CalcularValorTotal(PedidosCompras pedidoCompras, bool isEditar = false)
        {
            pedidoCompras.ValorTotal = pedidoCompras.ProdutosPedidos.Sum(produto => produto.Produto.ValorUnitario * produto.Quantidade);

            if (isEditar)
            {
                if (pedidoCompras.ValorTotal == 0) pedidoCompras.Status = 0;
                await _pedidosComprasRepository.UpdatePedidoAsync(pedidoCompras);
            }
            else await _pedidosComprasRepository.SalvarPedidoAsync(pedidoCompras);
        }
    }
}
