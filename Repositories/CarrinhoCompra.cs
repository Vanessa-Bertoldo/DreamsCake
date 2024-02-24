using lanchonete.Context;
using lanchonete.Models;

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
    }
}
