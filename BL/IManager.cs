using System;
using System.Collections;
using System.Collections.Generic;
using ClothingManager.BL.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace ClothingManager.BL{
    public interface IManager{
        public ClothingPiece GetClothingPiece(int id);
        public ClothingPiece GetClothingPieceWithStore(int id);

        public IEnumerable<ClothingPiece> GetAllClothingPieces();
        public IEnumerable<ClothingPiece> GetClothingPiecesOfPage(int currentPage, int pageSize);
        public int GetClothingPieceCount();

        public IEnumerable<ClothingPiece> GetClothingPiecesOfType(ClothingType clothingType);

        public ClothingPiece AddClothingPiece(double? price, DateTime manufactureDate, string color, ClothingType clothingType);

        public Designer GetDesigner(int id);
        public Designer GetDesignerWithClothingPieces(int id);
        public Designer GetDesignerWithClothingPiecesAndStore(int id);

        public IEnumerable<Designer> GetAllDesigners();

        public IEnumerable<Designer> GetDesignerByNameAndAge(string name, int? age);

        public Designer AddDesigner(string name, int age, string nationality);

        public IEnumerable<ClothingPiece> GetAllClothingPiecesWithStore();
        public IEnumerable<Designer> GetAllDesignersWithClothingPiecesAndStore();

        public IEnumerable<Store> GetAllStores();
        public Store GetStore(int id);
        public Store GetStoreWithClothingPieces(int id);
        public IEnumerable<Store> GetAllStoresWithClothingPieces();
        public Store AddStore(string city, int zipcode, string name);
        public Store ChangeStore(string city, int zipcode, string name, int id);
        public Store ChangeStoreWithPatch(int id, JsonPatchDocument<Store> patchDocument);

        public IEnumerable<ClothingPiece> GetClothingPiecesOfDesigner(int designerId);
        public IEnumerable<Designer> GetDesignersOfClothingPiece(int clothingPieceId);
        public IEnumerable<Designer> GetAvailableDesignersOfClothingPiece(int clothingPieceId);
        
        public ClothingPieceDesigner AddClothingPieceDesigner(ClothingPiece clothingPiece, Designer designer);
        public ClothingPieceDesigner AddClothingPieceDesigner(ClothingPiece clothingPiece, Designer designer, int contributionOrder);
        public void RemoveClothingPieceDesigner(int clothingPieceId, int designerId);

    }
}