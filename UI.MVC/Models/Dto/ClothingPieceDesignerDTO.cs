using System;
using System.ComponentModel.DataAnnotations;
using ClothingManager.BL.Domain;

namespace ClothingManager.UI.MVC.Models.Dto{
    public class ClothingPieceDesignerDTO{
        [Required] 
        public int ClothingPieceId{ get; set; }
        [Required] 
        public int DesignerId{ get; set; }
        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "ContributionOrder must be a positive number greater then 1")]
        public int ContributionOrder{ get; set; }

    }
}