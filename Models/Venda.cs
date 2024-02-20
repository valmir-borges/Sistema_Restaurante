using Sistema_Restaurante.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Venda")]
    public class Venda
    {
        [Column("VendaId")]
        [Display(Name = "Código da Venda")]

        public int Id { get; set; }

        [Column("ValorTotal")]
        [Display(Name = "Valor da Venda")]

        public decimal ValorTotal { get; set; }

        [Column("Data")]
        [Display(Name = "Data da venda")]

        public DateTime Data { get; set; }

        [ForeignKey("ClienteId")]
        [Display(Name = "Cliente")]

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        [ForeignKey("Garcom")]
        [Display(Name = "Garçom")]
        public int GarcomId { get; set; }
        public Garcom? Garcom { get; set; }

        [ForeignKey("PagamentoId")]
        [Display(Name = "Pagamento")]

        public int PagamentoId { get; set; }
        public Pagamento? Pagamento { get; set; }

        [NotMapped]
        public List<VendaHasProduto>? ProdutoList { get; set; }
    }
}
