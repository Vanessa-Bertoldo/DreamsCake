using lanchonete.Models;
using Microsoft.EntityFrameworkCore;
namespace lanchonete.Context
{
    public class AppDbContext :DbContext
    {
        //Define as classes do dominio que se deseja mapear
        //As strings de conexao com o banco sao definidas no arquivo appsettings.json

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set;}
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
    }
}
