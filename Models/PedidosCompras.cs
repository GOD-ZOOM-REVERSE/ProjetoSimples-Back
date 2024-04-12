using ProcessoManymindsBack.Migrations;
using System.ComponentModel.DataAnnotations;

namespace ProcessoManyminds_Back.Models
{
    public class PedidosCompras
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public virtual List<ProdutoPedido> ProdutosPedidos { get; set; }
        public double ValorTotal { get; set; }
        public StatusPedido Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public enum StatusPedido
        {
            Finalizado = 0,
            Ativo = 1
        }
    }

    public class ProdutoPedido
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int ProdutoCodigo { get; set; }
        public virtual Produtos Produto { get; set; }
        public int Quantidade { get; set; }
    }
}
