using lanchonete.Models;

namespace lanchonete.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        //Propiedade somente de leitura que retorna uma Lista de objetos categoria
        //IEnumerable expoe um enumerador que dá suporte a uma iteração simples em uma coleção não generica - Funciona apenas como leitura
        IEnumerable<Categoria> Categorias { get; }
    }
}
