using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lanchonete.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LancheId { get; set; }
        [Required(ErrorMessage = "Insira um nome valido")]
        [StringLength(150, ErrorMessage = "Tamanho excedido")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Insira um descrição curta valida")]
        [StringLength(150, ErrorMessage = "Tamanho excedido")]
        public string DescricaoCurta { get; set; }
        public string DescricaoDetalhada { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2")]
        [Range(1,999, ErrorMessage = "O preço deve estar entre 1 e 999")]
        public decimal Preco { get; set; }
        [StringLength(300, ErrorMessage = "Tamanho excedido")]
        public string ImagemUrl { get; set; }
        public bool IsLanchePreferido { get; set; }
        [Required]
        public bool EmEstoque { get; set; }
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
