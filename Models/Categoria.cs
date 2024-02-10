using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lanchonete.Models
{
    [Table("Categorias")] //Não é obrigatório uma vez que o mapeamento esta contino no Context, a não ser que queira usar outro nome para a tabela
    public class Categoria
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategooriaId { get; set; }
        [StringLength(150, ErrorMessage = "Tamanho máximo excedido")]
        [Required(ErrorMessage = "Insira um nome para a categoria")]
        public string CategoriaNome { get; set; }
        [StringLength(350)]
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; } //estabelece o relacionamento 1:N com lanches
    }
}
