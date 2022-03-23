using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClothingManager.DAL;
using ClothingManager.BL.Domain;
using Microsoft.AspNetCore.JsonPatch;

namespace ClothingManager.BL{
    public class Manager : IManager{
        private readonly IRepository _repository;

        public Manager(IRepository repository){
            _repository = repository;
        }

        public ClothingPiece GetClothingPiece(int id){
            return _repository.ReadClothingPiece(id);
        }

        public ClothingPiece GetClothingPieceWithStore(int id){
            return _repository.ReadClothingPieceWithStore(id);
        }

        public IEnumerable<ClothingPiece> GetAllClothingPieces(){
            return _repository.ReadAllClothingPieces();
        }

        public IEnumerable<ClothingPiece> GetClothingPiecesOfPage(int currentPage, int pageSize){
            int from = (currentPage * pageSize) - pageSize;
            return _repository.ReadClothingPiecesOfPage(from, pageSize);
        }

        public int GetClothingPieceCount(){
            return _repository.ReadClothingPiecesCount();
        }

        public IEnumerable<ClothingPiece> GetClothingPiecesOfType(ClothingType clothingType){
            return _repository.ReadClothingPiecesOfTypeWithStore(clothingType);
        }

        public ClothingPiece AddClothingPiece(double? price, DateTime manufactureDate, string color, ClothingType clothingType){
            ClothingPiece clothingPiece = new ClothingPiece(price, manufactureDate, color, clothingType);
            Validator.ValidateObject(clothingPiece, new  ValidationContext(clothingPiece), true);
            _repository.CreateClothingPiece(clothingPiece);
            return clothingPiece;
        }

        public Designer GetDesigner(int id){
            return _repository.ReadDesigner(id);
        }

        public Designer GetDesignerWithClothingPieces(int id){
            return _repository.ReadDesignerWithClothingPieces(id);
        }

        public Designer GetDesignerWithClothingPiecesAndStore(int id){
            return _repository.ReadDesignerWithClothingPiecesAndStore(id);
        }

        public IEnumerable<Designer> GetAllDesigners(){
            return _repository.ReadAllDesigners();
        }

        public IEnumerable<Designer> GetDesignerByNameAndAge(string name, int? age){
            return _repository.ReadDesignerByNameAndAgeWithClothingPiecesAndStore(name, age);
        }

        public Designer AddDesigner(string name, int age, string nationality){
            Designer designer = new Designer(name, age, nationality);
            Validator.ValidateObject(designer, new  ValidationContext(designer), true);
            _repository.CreateDesigner(designer);
            return designer;
        }

        public IEnumerable<ClothingPiece> GetAllClothingPiecesWithStore(){
            return _repository.ReadAllClothingPiecesWithStore();
        }

        public IEnumerable<Designer> GetAllDesignersWithClothingPiecesAndStore(){
            return _repository.ReadAllDesignersWithClothingPiecesAndStore();
        }

        public IEnumerable<Store> GetAllStores(){
            return _repository.ReadAllStores();
        }

        public Store GetStore(int id){
            return _repository.ReadStore(id);
        }

        public Store GetStoreWithClothingPieces(int id){
            return _repository.ReadStoreWithClothingPieces(id);
        }

        public IEnumerable<Store> GetAllStoresWithClothingPieces(){
            return _repository.ReadAllStoresWithClothingPieces();
        }

        public Store AddStore(string city, int zipcode, string name){
            Store store = new Store(city, zipcode, name);
            Validator.ValidateObject(store, new  ValidationContext(store), true);
            _repository.CreateStore(store);
            return store;
        }

        public Store ChangeStore(string city, int zipcode, string name, int id){
            Store store = new Store(city, zipcode, name, id);
            store.ClothingPieces = _repository.ReadStore(id).ClothingPieces;
            _repository.UpdateStore(store);
            return store;
        }

        public Store ChangeStoreWithPatch(string city, int? zipcode, string name, int id) {
            Store storeToUpdate = _repository.ReadStore(id);
            JsonPatchDocument<Store> patchEntity = new JsonPatchDocument<Store>();

            if (city is null) {
                patchEntity.Remove(s => s.City);
            } else if (string.IsNullOrEmpty(storeToUpdate.City)) {
                patchEntity.Add(s => s.City, city);
            } else if (city != storeToUpdate.City) {
                patchEntity.Replace(s => s.City, city);
            } 

            if (name is null) {
                patchEntity.Remove(s => s.Name);
            } else if (string.IsNullOrEmpty(storeToUpdate.Name)) {
                patchEntity.Add(s => s.Name, name);
            } else if (name != storeToUpdate.Name) {
                patchEntity.Replace(s => s.Name, name);
            }
            
            if (zipcode is null) {
                patchEntity.Remove(s => s.Zipcode);
            } else if (storeToUpdate.Zipcode == 0) {
                patchEntity.Add(s => s.Zipcode, zipcode);
            } else if (zipcode != storeToUpdate.Zipcode) {
                patchEntity.Replace(s => s.Zipcode, zipcode);
            }
            return _repository.UpdateStoreWithPatch(id, patchEntity);
        }

        public IEnumerable<ClothingPiece> GetClothingPiecesOfDesigner(int designerId){
            return _repository.ReadClothingPiecesOfDesigner(designerId);
        }

        public IEnumerable<Designer> GetDesignersOfClothingPiece(int clothingPieceId){
            return _repository.ReadDesignersOfClothingPiece(clothingPieceId);
        }

        public IEnumerable<Designer> GetAvailableDesignersOfClothingPiece(int clothingPieceId){
            return _repository.ReadAvailableDesignersOfClothingPiece(clothingPieceId);
        }

        public ClothingPieceDesigner AddClothingPieceDesigner(ClothingPiece clothingPiece, Designer designer){
            ClothingPieceDesigner cpd = new ClothingPieceDesigner();
            cpd.ClothingPiece = clothingPiece;
            cpd.Designer = designer;
            cpd.ContributionOrder = _repository.ReadDesignersOfClothingPiece(clothingPiece.Id).ToList().Count + 1;
            return _repository.CreateClothingPieceDesigner(cpd);
        }

        public ClothingPieceDesigner AddClothingPieceDesigner(ClothingPiece clothingPiece, Designer designer, int contributionOrder){
            ClothingPieceDesigner cpd = new ClothingPieceDesigner();
            cpd.ClothingPiece = clothingPiece;
            cpd.Designer = designer;
            cpd.ContributionOrder = contributionOrder;
            return _repository.CreateClothingPieceDesigner(cpd);
        }

        public void RemoveClothingPieceDesigner(int clothingPieceId, int designerId){
            _repository.DeleteClothingPieceDesigner(clothingPieceId, designerId);
        }

        public void RemoveStore(int storeId){
            _repository.DeleteStore(storeId);
        }
    }
}