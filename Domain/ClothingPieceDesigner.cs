using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.BL.Domain{
    public class ClothingPieceDesigner{
        [Required] 
        public ClothingPiece ClothingPiece{ get; set; }
        [Required] 
        public Designer Designer{ get; set; }
        [Range(1, Int32.MaxValue, ErrorMessage = "ContributionOrder must be a positive number greater then 1")]
        public int ContributionOrder{ get; set; }
        
    }
    
}