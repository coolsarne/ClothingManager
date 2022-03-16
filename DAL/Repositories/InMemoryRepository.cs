using System;
using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL.Domain;
using ClothingManager.DAL.EF;

namespace ClothingManager.DAL{
    public class InMemoryRepository : IRepository{
        private List<ClothingPiece> _clothingPieces;
        private List<Designer> _designers;

        public InMemoryRepository(){
            _clothingPieces = new List<ClothingPiece>();
            _designers = new List<Designer>();
            Seed();
        }
        
        private void Seed(){
            ClothingPiece piece1 = new ClothingPiece(79.99, new DateTime(2020, 12, 10), "black", ClothingType.Tshirt);
            ClothingPiece piece2 = new ClothingPiece(299.99, new DateTime(2021, 9, 1), "red", ClothingType.Jacket);
            ClothingPiece piece3 = new ClothingPiece(549.98, new DateTime(2021, 9, 21), "white", ClothingType.Shoes);
            ClothingPiece piece4 = new ClothingPiece(199.50, new DateTime(2021, 4, 6), "black", ClothingType.Sweater);

            Designer designer1 = new Designer("Raf Simons", 53, "Belgian");
            Designer designer2 = new Designer("Ralph Lauren", 81, "American");
            Designer designer3 = new Designer("Louis Vuitton", 167, "French");
            Designer designer4 = new Designer("Coco Chanel", 87, "French");

            Store store1 = new Store("Paris", 75000, "Lorette & Jasmin", 2222);
            Store store2 = new Store("Milan", 20019, "DMAG outlet", 1111);

            piece1.Store = store1;
            piece2.Store = store1;
            piece3.Store = store2;
            piece4.Store = store2;
            
            // piece1.Designers = new List<Designer>(){designer1, designer2};
            // piece2.Designers = new List<Designer>(){designer3};
            // piece3.Designers = new List<Designer>(){designer2, designer4};
            // piece4.Designers = new List<Designer>(){designer3};
            // designer1.ClothingPieces = new List<ClothingPiece>(){piece1};
            // designer2.ClothingPieces = new List<ClothingPiece>(){piece1, piece3};
            // designer3.ClothingPieces = new List<ClothingPiece>(){piece2, piece4};
            // designer4.ClothingPieces = new List<ClothingPiece>(){piece3};
            
            ClothingPieceDesigner cpd1 = new ClothingPieceDesigner(){ClothingPiece = piece1, Designer = designer1, ContributionOrder = 1};
            ClothingPieceDesigner cpd2 = new ClothingPieceDesigner(){ClothingPiece = piece1, Designer = designer2, ContributionOrder = 2};
            ClothingPieceDesigner cpd3 = new ClothingPieceDesigner(){ClothingPiece = piece2, Designer = designer3};
            ClothingPieceDesigner cpd4 = new ClothingPieceDesigner(){ClothingPiece = piece3, Designer = designer2, ContributionOrder = 1};
            ClothingPieceDesigner cpd5 = new ClothingPieceDesigner(){ClothingPiece = piece3, Designer = designer4, ContributionOrder = 2};
            ClothingPieceDesigner cpd6 = new ClothingPieceDesigner(){ClothingPiece = piece4, Designer = designer3};
            
            store1.ClothingPieces = new List<ClothingPiece>(){piece1, piece2};
            store2.ClothingPieces = new List<ClothingPiece>(){piece3, piece4};
            
            CreateClothingPiece(piece1);
            CreateClothingPiece(piece2);
            CreateClothingPiece(piece3);
            CreateClothingPiece(piece4);

            CreateDesigner(designer1);
            CreateDesigner(designer2);
            CreateDesigner(designer3);
            CreateDesigner(designer4);
        }

        public ClothingPiece ReadClothingPiece(int id){
            foreach (ClothingPiece clothingPiece in _clothingPieces){
                if (clothingPiece.Id == id){
                    return clothingPiece;
                }
            }

            return null;
        }

        public ClothingPiece ReadClothingPieceWithStore(int id){
            throw new NotImplementedException();
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPieces(){
            return _clothingPieces;
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfPage(int @from, int to){
            throw new NotImplementedException();
        }

        public int ReadClothingPiecesCount(){
            throw new NotImplementedException();
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfTypeWithStore(ClothingType clothingType){
            ICollection<ClothingPiece> pieces = new List<ClothingPiece>();
            foreach (ClothingPiece clothingPiece in _clothingPieces){
                if (clothingPiece.ClothingType.Equals(clothingType)){
                    pieces.Add(clothingPiece);
                }
            }

            return pieces;
        }

        public ClothingPiece CreateClothingPiece(ClothingPiece clothingPiece){
            _clothingPieces.Add(clothingPiece);
            _clothingPieces.Last().Id = _clothingPieces.Count;
            return clothingPiece;
        }

        public Designer ReadDesigner(int id){
            foreach (Designer designer in _designers){
                if (designer.Id == id){
                    return designer;
                }
            }

            return null;
        }

        public Designer ReadDesignerWithClothingPieces(int id){
            throw new NotImplementedException();
        }

        public Designer ReadDesignerWithClothingPiecesAndStore(int id){
            throw new NotImplementedException();
        }

        public IEnumerable<Designer> ReadAllDesigners(){
            return _designers;
        }

        public IEnumerable<Designer> ReadDesignerByNameAndAgeWithClothingPiecesAndStore(string name, int? age){
            IQueryable<Designer> results = _designers.AsQueryable();

            if (!String.IsNullOrEmpty(name)){
                results = results.Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            if (age != null){
                results = results.Where(d => d.Age == age);
            }
            
            return results;

        }

        public Designer CreateDesigner(Designer designer){
            _designers.Add(designer);
            _designers.Last().Id = _designers.Count;
            return designer;
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithStore(){
            throw new NotImplementedException();
        }

        public IEnumerable<Designer> ReadAllDesignersWithClothingPiecesAndStore(){
            throw new NotImplementedException();
        }

        public IEnumerable<Store> ReadAllStores(){
            throw new NotImplementedException();
        }

        public Store ReadStore(int id){
            throw new NotImplementedException();
        }

        public Store ReadStoreWithClothingPieces(int id){
            throw new NotImplementedException();
        }

        public IEnumerable<Store> ReadAllStoresWithClothingPieces(){
            throw new NotImplementedException();
        }

        public Store UpdateStore(Store store){
            throw new NotImplementedException();
        }

        public Store UpdateStore(string city, int zipcode, string name, int id){
            throw new NotImplementedException();
        }

        public Store CreateStore(Store store){
            throw new NotImplementedException();
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithDesigners(){
            throw new NotImplementedException();
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfDesigner(int designerId){
            throw new NotImplementedException();
        }

        public IEnumerable<Designer> ReadDesignersOfClothingPiece(int clothingPieceId){
            throw new NotImplementedException();
        }

        public IEnumerable<Designer> ReadAvailableDesignersOfClothingPiece(int clothingPieceId){
            throw new NotImplementedException();
        }

        public ClothingPieceDesigner CreateClothingPieceDesigner(ClothingPieceDesigner clothingPieceDesigner){
            throw new NotImplementedException();
        }

        public void DeleteClothingPieceDesigner(int clothingPieceId, int authorId){
            throw new NotImplementedException();
        }
    }
}