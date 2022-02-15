using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers.Api{
    public class StoreController : Controller{
        public IActionResult Index(){
            return View();
        }

        public IActionResult Details(){
            return View();
        }
    }
}