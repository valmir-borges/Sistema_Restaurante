using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Restaurante.Models
{
    [Table("Garcom")]
    public class Garcom
    {
        [Column("GarcomId")]
        [Display(Name = "Código do Garçom")]

        public int Id { get; set; }

        [Column("NomeGarcom")]
        [Display(Name = "Nome do Garçom")]

        public string NomeGarcom { get; set; } = string.Empty;

        [Column("IdadeGarcom")]
        [Display(Name = "Idade do Garçom")]

        public int IdadeGarcom { get; set; }
    }
}
