using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Column("CategoriaId")]
        [Display(Name = "Código da Categoria")]

        public int Id { get; set; }

        [Column("NomeCategoria")]
        [Display(Name = "Nome da Categoria")]

        public string NomeCategia { get; set; } = string.Empty;
    }
}
