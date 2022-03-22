using System.Collections;
using System.Collections.Generic;
using ClothingManager.BL.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace ClothingManager.DAL{
    
    public interface IRepository{
        public ClothingPiece ReadClothingPiece(int id);
        public ClothingPiece ReadClothingPieceWithStore(int id);
        public IEnumerable<ClothingPiece> ReadAllClothingPieces();
        public IEnumerable<ClothingPiece> ReadClothingPiecesOfPage(int from, int to);
        public int ReadClothingPiecesCount();

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfTypeWithStore(ClothingType clothingType);

        public ClothingPiece CreateClothingPiece(ClothingPiece clothingPiece);

        public Designer ReadDesigner(int id);
        public Designer ReadDesignerWithClothingPieces(int id);
        public Designer ReadDesignerWithClothingPiecesAndStore(int id);

        public IEnumerable<Designer> ReadAllDesigners();

        public IEnumerable<Designer> ReadDesignerByNameAndAgeWithClothingPiecesAndStore(string name, int? age);

        public Designer CreateDesigner(Designer designer);
        
        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithStore();
        public IEnumerable<Designer> ReadAllDesignersWithClothingPiecesAndStore();

        public IEnumerable<Store> ReadAllStores();
        public Store ReadStore(int id);
        public Store ReadStoreWithClothingPieces(int id);
        public IEnumerable<Store> ReadAllStoresWithClothingPieces();
        public Store UpdateStore(Store store);
        public Store UpdateStoreWithPatch(int id, JsonPatchDocument<Store> patchDocument);
        public Store CreateStore(Store store);

        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithDesigners();

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfDesigner(int designerId);
        public IEnumerable<Designer> ReadDesignersOfClothingPiece(int clothingPieceId);
        public IEnumerable<Designer> ReadAvailableDesignersOfClothingPiece(int clothingPieceId);

        public ClothingPieceDesigner CreateClothingPieceDesigner(ClothingPieceDesigner clothingPieceDesigner);
        public void DeleteClothingPieceDesigner(int clothingPieceId, int designerId);
        
        public ClothingPiece DeleteClothingPiece(ClothingPiece clothingPiece);
        public Designer DeleteDesigner(Designer designer);
        public List<Designer> FilterByNationality(string nationality);


    }
}