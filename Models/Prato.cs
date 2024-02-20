using Sistema_Restaurante.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Prato")]
    public class Prato
    {
        [Column("PratoId")]
        [Display(Name = "Código do Prato")]

        public int Id { get; set; }

        [Column("NomePrato")]
        [Display(Name = "Nome do Prato")]

        public string NomePrato { get; set; } = string.Empty;

        [Column("PrecoPrato")]
        [Display(Name = "Preço do Prato")]

        public double PrecoPrato { get; set; }

        [ForeignKey("CategoriaId")]
        [Display(Name = "Categoria")]

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }


        [ForeignKey("FornecedorId")]
        [Display(Name = "Fornecedor")]

        public int FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
}
