using lanchonete.Context;
using lanchonete.Models;
using Microsoft.EntityFrameworkCore;

namespace lanchonete.Repositories
{
    public class CarrinhoCompra
    {
        public readonly AppDbContext _context;


        //injeta o contexto no contrutor
        public CarrinhoCompra(AppDbContext context)
        {
            //Com isso podemos remover ou excluir itens do carrinho
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
        public static CarrinhoCompra GetCarrinho(IServiceProvider services) //Essa interface permite definir uma sessão
        {
            // Verifica se a interface IHttpContextAccessor está disponível no serviço
            // e obtém a sessão atual do usuário, se possível
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            // Obtém o contexto do aplicativo para acessar o banco de dados
            var context = services.GetService<AppDbContext>();

            // Obtém ou gera um ID único para o carrinho de compras
            // usando a sessão do usuário, se disponível
            string carrinhoId = session.GetString("carrinhoId") ?? Guid.NewGuid().ToString();

            //Obtem o id do carrinho na sessão
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna uma instância de CarrinhoCompra com o contexto do banco de dados
            // e o ID atribuído ou obtido para o carrinho de compras
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId,
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            // Verifica se o item do carrinho já existe com base no ID do lanche e no ID do carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            // Se o item do carrinho não existir, cria um novo
            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    // Define o ID do carrinho, o lanche e a quantidade
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                // Adiciona o novo item do carrinho ao contexto
                _context.CarrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else
            {
                // Se o item do carrinho já existir, aumenta a quantidade
                carrinhoCompraItem.Quantidade++;
            }
        }

        public void RemoverDoCarrinho(Lanche lanche)
        {
            // Verifica se o item do carrinho já existe com base no ID do lanche e no ID do carrinho
            var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault(
                s => s.Lanche.LancheId == lanche.LancheId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    // Se a quantidade do item for maior que 1, decrementa a quantidade
                    carrinhoCompraItem.Quantidade--;
                } 
                else
                {
                    // Se não, remove do o item do carrinho
                    _context.CarrinhoCompraItems.Remove(carrinhoCompraItem);
                }
            }

            // Persiste as alterações no banco de dados
            _context.SaveChanges(); 
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItems()
        { 
            // Retorna a lista de itens do carrinho de compras se já estiver carregada,
          // caso contrário, carrega os itens do banco de dados e os retorna
            return CarrinhoCompraItems ?? // Verifica se a lista de itens do carrinho já está carregada
                (CarrinhoCompraItems = // Se não estiver carregada, carrega os itens do banco de dados
                    _context.CarrinhoCompraItems // Consulta os itens do carrinho de compras no banco de dados
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId) // Filtra os itens pelo ID do carrinho atual
                    .Include(s => s.Lanche) // Inclui os dados do lanche relacionado aos itens do carrinho
                    .ToList()); // Converte os resultados em uma lista e retorna
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItems  // Obtém os itens do carrinho com base no ID do carrinho atual
                .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoCompraItems.RemoveRange(carrinhoItens);  // Remove todos os itens do carrinho obtidos
            _context.SaveChanges(); // Persiste as alterações no banco de dados
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItems // Acessa a tabela de itens do carrinho no banco de dados
                    .Where(c => c.CarrinhoCompraId == CarrinhoCompraId) // Filtra os itens pelo ID do carrinho atual
                    .Select(c => c.Lanche.Preco * c.Quantidade) // Calcula o preço total de cada item (preço do lanche multiplicado pela quantidade)
                    .Sum(); // Soma os preços totais de todos os itens do carrinho

            return total;
        }
    }
}
