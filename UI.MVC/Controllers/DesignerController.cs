using System.Collections.Generic;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers{
    public class DesignerController : Controller{
        private readonly IManager _manager;

        public DesignerController(IManager manager){
            _manager = manager;
        }
        
        // GET: /Designer
        public IActionResult Index(){
            IEnumerable<Designer> designers = _manager.GetAllDesigners();
            return View(designers);
        }
        
        // GET: /Designer/Details/<Id>
        public IActionResult Details(int designerId){
            Designer designer = _manager.GetDesignerWithClothingPieces(designerId);
            return View(designer);
        }
        
        // GET: /Designer/Add
        [HttpGet]
        public IActionResult Add(){
            return View();
        }
        
        // POST: /Designer/Add
        [HttpPost]
        public IActionResult Add(Designer designer){
            if (!ModelState.IsValid) return View(designer);
            Designer newDesigner = _manager.AddDesigner(designer.Name, designer.Age, designer.Nationality);
            return RedirectToAction("Details", "Designer", new{designerId = newDesigner.Id});
        }
    }
}