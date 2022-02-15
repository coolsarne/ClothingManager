using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.BL.Domain{
    public class Designer {
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(maximumLength:50, MinimumLength = 2, ErrorMessage = "Name length must be in between the range of 2 and 50 characters long")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Name can only contain alphabetical characters")]
        public string Name{ get; set; }
        
        [Required(ErrorMessage = "Age is required")]
        [Range(1, 150, ErrorMessage = "Age must be in between the range of 1 and 150")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Nationality is required")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "Nationality can only contain alphabetical characters")]
        public string Nationality{ get; set; }
        
        public ICollection<ClothingPieceDesigner> ClothingPieces{ get; set; }
        
        [Key]
        public int Id{ get; set; }
        
        public Designer(string name, int age, string nationality){
            Name = name;
            Age = age;
            Nationality = nationality;
            
        }

        public Designer(){}
    }
}