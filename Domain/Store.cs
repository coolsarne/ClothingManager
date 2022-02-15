using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClothingManager.BL.Domain{
    public class Store{
        public string City{ get; set; }
        public int Zipcode{ get; set; }
        public string Name{ get; set; }
        public ICollection<ClothingPiece> ClothingPieces{ get; set; }
        [Key] public int Id{ get; set; }

        public Store(string city, int zipcode, string name, int id){
            City = city;
            Zipcode = zipcode;
            Name = name;
            Id = id;
        }
        
        public Store(string city, int zipcode, string name){
            City = city;
            Zipcode = zipcode;
            Name = name;
        }

        public Store(){}
        
    }
}