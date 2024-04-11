using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProcessoManyminds_Back.Models
{
    public class Produtos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public double ValorUnitario { get; set; }
        public bool IsInativo { get; set; }
    }
}
