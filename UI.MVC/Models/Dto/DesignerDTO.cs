using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ClothingManager.BL.Domain;

namespace ClothingManager.UI.MVC.Models.Dto{
    public class DesignerDTO{
        public string Name{ get; set; }
        public int Age { get; set; }
        public string Nationality{ get; set; }
        public int Id{ get; set; }

        public DesignerDTO(Designer designer){
            Name = designer.Name;
            Age = designer.Age;
            Nationality = designer.Nationality;
            Id = designer.Id;
        }
    }
}