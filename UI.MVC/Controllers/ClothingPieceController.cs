using System.Collections.Generic;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers{
    public class ClothingPieceController : Controller{
        private readonly IManager _manager;

        public ClothingPieceController(IManager manger){
            _manager = manger;
        }
        
        // GET: /ClothingPiece
        public IActionResult Details(int clothingPieceId){
            ClothingPiece clothingPiece = _manager.GetClothingPieceWithStore(clothingPieceId);
            return View(clothingPiece);
        }

        public IActionResult Overview(){
            IEnumerable<ClothingPiece> clothingPieces = _manager.GetAllClothingPieces();
            return View(clothingPieces);
        }
    }
}