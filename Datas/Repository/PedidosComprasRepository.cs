using Microsoft.EntityFrameworkCore;
using ProcessoManyminds_Back.Context;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;
using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Datas.Repository
{
    public class PedidosComprasRepository : IPedidosComprasRepository
    {
        private readonly ManymindsContext _context;

        public PedidosComprasRepository(ManymindsContext context)
        {
            _context = context;
        }

        public async Task<List<PedidosCompras>> GetPedidosAsync()
        {
            return await _context.PedidosCompras.Include(i => i.ProdutosPedidos).ThenInclude(i => i.Produto).ToListAsync();
        }
        public async Task<PedidosCompras?> GetPedidosByIdAsync(string id)
        {
            return await _context.PedidosCompras.Include(i => i.ProdutosPedidos).ThenInclude(i => i.Produto).FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task SalvarPedidoAsync(PedidosCompras pedidos)
        {
            pedidos.ProdutosPedidos = pedidos.ProdutosPedidos.Select(s => new ProdutoPedido
            {
                Id = s.Id,
                ProdutoCodigo = s.Produto.Codigo,
                Quantidade = s.Quantidade
            }).ToList();

            await _context.PedidosCompras.AddAsync(pedidos);
            await _context.SaveChangesAsync();
        }
        public async Task UpdatePedidoAsync(PedidosCompras pedidos)
        {
            pedidos.UpdatedAt = DateTime.Now;
            //_context.Entry(pedidos).CurrentValues.SetValues(pedidos);
            _context.PedidosCompras.Update(pedidos).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
