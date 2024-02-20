using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Fornecedor")]
    public class Fornecedor
    {
        [Column("FornecedorId")]
        [Display(Name = "Código do Fornecedor")]

        public int Id { get; set; }

        [Column("NomeFornecedor")]
        [Display(Name = "Nome do Fornecedor")]

        public string NomeFornecedor { get; set; } = string.Empty;

        [Column("CnpjFornecedor")]
        [Display(Name = "Cnpj do Fornecedor")]

        public string NomeName { get; set; } = string.Empty;
    }
}
