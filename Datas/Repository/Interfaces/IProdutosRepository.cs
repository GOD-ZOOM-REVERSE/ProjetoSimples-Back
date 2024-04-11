using ProcessoManyminds_Back.Models;

namespace ProcessoManyminds_Back.Datas.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        Task<List<Produtos>> GetProdutosAsync();
        Task<Produtos?> GetProdutosByCodigoAsync(int codigo);
        Task SalvarAsync(Produtos produtos);
        Task UpdateAsync(Produtos produtos);
    }
}
