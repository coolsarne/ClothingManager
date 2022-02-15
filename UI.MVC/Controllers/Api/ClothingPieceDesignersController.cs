using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ClothingManager.UI.MVC.Controllers.Api{
    
    [ApiController]
    [Route("api/clothingPieceDesigners")]
    public class ClothingPieceDesignersController : ControllerBase{
        private readonly IManager _mgr;

        public ClothingPieceDesignersController(IManager mgr){
            _mgr = mgr;
        }
        
        
        [HttpGet("{clothingPieceId:int}/availableDesigners")]
        public IActionResult Get(int clothingPieceId){
            IEnumerable<Designer> designers = _mgr.GetAvailableDesignersOfClothingPiece(clothingPieceId);
            if (designers is null) return BadRequest();
            if (!designers.Any()) return NoContent();
            return Ok(designers.Select(d => new DesignerDTO(d)));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClothingPieceDesignerDTO dto){
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Designer designer = _mgr.GetDesigner(dto.DesignerId);
            ClothingPiece clothingPiece = _mgr.GetClothingPieceWithStore(dto.ClothingPieceId);
            _mgr.AddClothingPieceDesigner(clothingPiece, designer, dto.ContributionOrder);
            return CreatedAtAction("Get",new{clothingPieceId=dto.ClothingPieceId});
        }
    }
}