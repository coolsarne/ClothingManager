using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.BL.Domain{
    public class ClothingPiece : IValidatableObject{
        
        [Range(0, Double.MaxValue, ErrorMessage = "Price must be a positive number")]
        public double? Price{ get; set; }

        public DateTime ManufactureDate{ get; set; }

        [Required(ErrorMessage = "Color is required")]
        [RegularExpression(@"^[a-zA-Z ]*", ErrorMessage = "Color can only contain alphabetical characters")]
        public string Color{ get; set; }
        
        [Required(ErrorMessage = "ClothingType is required")]
        public ClothingType ClothingType{ get; set; }
        
        public Store Store{ get; set; }
        
        public ICollection<ClothingPieceDesigner> Designers{ get; set; }

        [Key] public int Id{ get; set; }
        
        public ClothingPiece(double? price, DateTime manufactureDate, string color, ClothingType clothingType){
            Price = price;
            ManufactureDate = manufactureDate;
            Color = color;
            ClothingType = clothingType;
        }
    
        public ClothingPiece(){}

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext){
            IList<ValidationResult> results = new List<ValidationResult>(); 
            if (ManufactureDate > DateTime.Today){
                results.Add(
                    new ValidationResult("Manufacturedate must be in the past", new string[]{"ManufactureDate"}));
            }

            DateTime emptyDate = new DateTime(1, 1, 1, 0,0,0);
            if (ManufactureDate == emptyDate){
                results.Add(new ValidationResult("ManufactureDate must be filled in properly (dd/mm/yyyy)", memberNames: new string[]{"ManufactureDate"}));
            }
            
            return results;
        }
    }
}