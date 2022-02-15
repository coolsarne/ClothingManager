using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.UI.MVC.Models.Dto{
    public class StoreDTO{
        [Required]public string City{ get; set; }
        [Range(0, Int32.MaxValue, ErrorMessage = "Zipcode must be a positive number greater then 0")]
        [Required]public int Zipcode{ get; set; }
        [Required]public string Name{ get; set; }
        [Required]public int Id{ get; set; }
    }
}