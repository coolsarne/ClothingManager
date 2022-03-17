using System;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.UI.MVC.Models.Dto {
    public class StorePatchDTO {
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string Name { get; set; }
        [Required] public int Id { get; set; }
    }
}