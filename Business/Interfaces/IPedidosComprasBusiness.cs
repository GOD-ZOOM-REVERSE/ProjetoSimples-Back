using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Business.Interfaces
{
    public interface IPedidosComprasBusiness
    {
        Task CalcularValorTotal(PedidosCompras pedidoCompras, bool isEditar = false);
    }
}
