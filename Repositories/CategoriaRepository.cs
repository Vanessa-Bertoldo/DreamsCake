using lanchonete.Context;
using lanchonete.Models;
using lanchonete.Repositories.Interfaces;

namespace lanchonete.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        // Usamos a injeção de dependência nativa da ASP.NET Core para injetar uma instância do serviço do contexto 
        // representado pela classe AppDbContext no construtor da classe.
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context) // Construtor da classe que recebe uma instância de AppDbContext.
        {
            _context = context; // Atribui a instância do contexto recebida como parâmetro à variável de membro _context.
        }

        // Define uma propriedade pública chamada Categorias que retorna uma coleção de categorias.
        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
