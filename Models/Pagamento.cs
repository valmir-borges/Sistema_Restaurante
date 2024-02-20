using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Pagamento")]
    public class Pagamento
    {
        [Column("PagamentoId")]
        [Display(Name = "Código do Pagamento")]

        public int Id { get; set; }

        [Column("FormaPagamento")]
        [Display(Name = "Tipo de Pagamento")]

        public string FormaPagamento { get; set; } = string.Empty;
    }
}
