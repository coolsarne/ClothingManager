using System;
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
            if (pg < 1) pg = 1;
            const int pageSize = 5;

            IEnumerable<ClothingPiece> clothingPieces = _manager.GetClothingPiecesOfPage(pg, pageSize);

            int recsCount = _manager.GetClothingPieceCount();

            ClothingPiecePager pager = new ClothingPiecePager(recsCount, pg, pageSize, clothingPieces);

            this.ViewBag.Pager = pager;

            return View(clothingPieces);
        }

        // public IActionResult EditClothingPiece() {
        //     return View();
        // }
    }
}