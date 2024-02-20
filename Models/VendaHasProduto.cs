using Microsoft.EntityFrameworkCore;
using Sistema_Restaurante.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    //[Keyless]
    [Table("VendaHasProduto")]
    public class VendaHasProduto
    {
        [Column("VendaHasProdutoId")]
        [Display(Name = "Código da Venda do Prato")]

        public int Id { get; set; }

        [ForeignKey("VendaId")]

        public int VendaId { get; set; }
        public Venda? Venda { get; set; }

        [ForeignKey("PratoId")]

        public int PratoId { get; set; }
        public Prato? Prato { get; set; }
    }
}
