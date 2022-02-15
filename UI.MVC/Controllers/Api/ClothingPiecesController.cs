using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers.Api{
    
    [ApiController]
    [Route("api/clothingPieces")]
    public class ClothingPiecesController : ControllerBase{
        private readonly IManager _mgr;

        public ClothingPiecesController(IManager mgr){
            _mgr = mgr;
        }

        [HttpGet("{clothingPieceId:int}/designers")]
        public IActionResult Get(int clothingPieceId){
            IEnumerable<Designer> designers = _mgr.GetDesignersOfClothingPiece(clothingPieceId);
            if (designers is null) return BadRequest();
            if (!designers.Any()) return NoContent();
            return Ok(designers.Select(d => new DesignerDTO(d)));
        }
        
    }
}