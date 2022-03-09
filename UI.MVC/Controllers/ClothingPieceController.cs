using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Utils;
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

        public IActionResult Overview(int pg = 1){
            IEnumerable<ClothingPiece> clothingPieces = _manager.GetAllClothingPieces().ToList();

            const int pageSize = 5;
            if (pg < 1) pg = 1;
            int recsCount = clothingPieces.Count();

            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;

            var data = clothingPieces.Skip(recSkip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }
    }
}