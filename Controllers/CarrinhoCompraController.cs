using lanchonete.Repositories;
using lanchonete.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lanchonete.Controllers
{
    public class CarrinhoCompraController : Controller 
    {
        private readonly ILanchesRepository _lacheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILanchesRepository lacheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lacheRepository = lacheRepository;
            _carrinhoCompra = carrinhoCompra;
        }   

        public IActionResult Index()
        {
            return View();
        }
    }
}
