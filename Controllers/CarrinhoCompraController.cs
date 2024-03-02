using lanchonete.Repositories;
using lanchonete.Repositories.Interfaces;
using lanchonete.ViewModel;
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
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = itens;
            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
            };
            return View();
        }
    }
}
