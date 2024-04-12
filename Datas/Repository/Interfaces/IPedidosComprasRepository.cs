using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Datas.Repository.Interfaces
{
    public interface IPedidosComprasRepository
    {
        Task<List<PedidosCompras>> GetPedidosAsync();
        Task<PedidosCompras?> GetPedidosByIdAsync(string id);
        Task SalvarPedidoAsync(PedidosCompras pedidos);
        Task UpdatePedidoAsync(PedidosCompras pedidos);
    }
}
