using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProdutoApi {
    public class HomeController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
