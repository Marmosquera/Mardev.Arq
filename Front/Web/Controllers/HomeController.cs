using Mardev.Arq.Front.Web.Models;
using Mardev.Arq.Front.Web.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mardev.Arq.Front.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IProductsService productsService,
            ILogger<HomeController> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _productsService.GetAllProducts();
            var model = response.Result?.Items;
            if (model == null)
            {
                TempData["error"] = response?.Message;
            }

            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
