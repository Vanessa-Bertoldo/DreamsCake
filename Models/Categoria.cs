namespace lanchonete.Models
{
    public class Categoria
    {
        public int CategooriaId { get; set; }
        public string CategoriaNome { get; set; }
        public string Descricao { get; set; }
        public List<Lanche> Lanches { get; set; } //estabelece o relacionamento 1:N com lanches
    }
}
