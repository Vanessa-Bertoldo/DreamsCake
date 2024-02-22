using lanchonete.Repositories.Interfaces;
using lanchonete.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace lanchonete.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILanchesRepository _lancheRepository;
        public LancheController(ILanchesRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List()
        {
            //O tempo de vida do ViewData é o request. É mais rápida que as demais
            ViewData["Titulo"] = "Todos os Lanches";
            ViewData["Data"] = DateTime.Now;
            //ViewBag é uma propriedade dinâmica, armazena dados no formato chave/valor e não requer a conversão de tipos
            //Também possui o tempo de vida request
            //ViewBag.Chave = "valor";

            //TempData tranfere dados entre controller e view e metodos actions. Armazenas valores formato chave/valor.
            //Armazena os dados temporariamente e os remove automaticamente após recuperar um valor. Exige a conversão de tipos
            //TempData["Chave"] = "valor";

            //var lanches = _lancheRepository.Lanches;
            LanchesListViewModel model = new LanchesListViewModel();
            model.Lanches = _lancheRepository.Lanches;
            model.CategoriaAtual = "Categoria";
            return View(model);
        }
    }
}
