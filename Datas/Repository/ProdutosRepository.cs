using Microsoft.EntityFrameworkCore;
using ProcessoManyminds_Back.Context;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;
using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Datas.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly ManymindsContext _context;

        public ProdutosRepository(ManymindsContext context)
        {
            _context = context;
        }

        public async Task<List<Produtos>> GetProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produtos?> GetProdutosByCodigoAsync(int codigo)
        {
            return await _context.Produtos.FirstOrDefaultAsync(f => f.Codigo == codigo);
        }

        public async Task SalvarAsync(Produtos produtos)
        {
            await _context.Produtos.AddAsync(produtos);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Produtos produtos)
        {
            _context.Produtos.Update(produtos).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
