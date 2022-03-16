using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClothingManager.DAL.EF{
    public class Repository : IRepository{
        private readonly ClothingManagerDbContext _context;

        public Repository(ClothingManagerDbContext context){
            _context = context;
        }

        public ClothingPiece ReadClothingPiece(int id){
            return _context.ClothingPieces.Find(id);
        }

        public ClothingPiece ReadClothingPieceWithStore(int id){
            return _context.ClothingPieces
                .Include(cp => cp.Store)
                .Single(cp => cp.Id == id);
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPieces(){
            return _context.ClothingPieces;
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfPage(int from, int to){
            return _context.ClothingPieces.Skip(from).Take(to);
        }

        public int ReadClothingPiecesCount(){
            return _context.ClothingPieces.Count();
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfTypeWithStore(ClothingType clothingType){
            return _context.ClothingPieces.AsQueryable()
                .Where(piece => piece.ClothingType.Equals(clothingType))
                .Include(cp => cp.Store);
        }

        public ClothingPiece CreateClothingPiece(ClothingPiece clothingPiece){
            _context.ClothingPieces.Add(clothingPiece);
            _context.SaveChanges();
            return clothingPiece;
        }

        public Designer ReadDesigner(int id){
            return _context.Designers.Find(id);
        }

        public Designer ReadDesignerWithClothingPieces(int id){
            return _context.Designers
                .Include(d => d.ClothingPieces)
                .ThenInclude(cpd => cpd.ClothingPiece)
                .Single(d => d.Id == id);
        }

        public Designer ReadDesignerWithClothingPiecesAndStore(int id){
            return _context.Designers
                .Include(d => d.ClothingPieces)
                .ThenInclude(cpd => cpd.ClothingPiece.Store)
                .Include(d => d.ClothingPieces)
                .Single(d => d.Id == id);
        }

        public IEnumerable<Designer> ReadAllDesigners(){
            return _context.Designers;
        }

        public IEnumerable<Designer> ReadDesignerByNameAndAgeWithClothingPiecesAndStore(string name, int? age){
            IQueryable<Designer> results = ReadAllDesignersWithClothingPiecesAndStore().AsQueryable();
            if (!String.IsNullOrEmpty(name)){
                results = results.AsQueryable().Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            if (age != null){
                results = results.AsQueryable().Where(d => d.Age == age);
            }

            return results;
        }

        public Designer CreateDesigner(Designer designer){
            designer.Id = _context.Designers.Count() + 1;
            _context.Designers.Add(designer);
            _context.SaveChanges();
            return designer;
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithStore(){
            return _context.ClothingPieces.Include(s => s.Store);
        }

        public IEnumerable<Designer> ReadAllDesignersWithClothingPiecesAndStore(){
            return _context.Designers
                .Include(d => d.ClothingPieces)
                .ThenInclude(cpd => cpd.ClothingPiece.Store)
                .Include(d => d.ClothingPieces);
        }

        public IEnumerable<Store> ReadAllStores(){
            return _context.Stores;
        }

        public Store ReadStore(int id){
            return _context.Stores.Find(id);
        }

        public Store ReadStoreWithClothingPieces(int id){
            return _context.Stores
                .Include(s => s.ClothingPieces)
                .Single(store => store.Id == id);
        }

        public IEnumerable<Store> ReadAllStoresWithClothingPieces(){
            return _context.Stores
                .Include(s => s.ClothingPieces);
        }


        public Store UpdateStore(Store store){
            var storeFromRepo = ReadStore(store.Id);
            if (storeFromRepo != null){
                storeFromRepo.City = store.City;
                storeFromRepo.Zipcode = store.Zipcode;
                storeFromRepo.Name = store.Name;
                storeFromRepo.ClothingPieces = store.ClothingPieces;
            }

            _context.SaveChanges();
            return storeFromRepo;
        }

        public Store CreateStore(Store store){
            store.Id = _context.Stores.Count() + 1;
            _context.Stores.Add(store);
            _context.SaveChanges();
            return store;
        }

        public IEnumerable<ClothingPiece> ReadAllClothingPiecesWithDesigners(){
            return _context.ClothingPieces
                .Include(cp => cp.Designers)
                .ThenInclude(cpd => cpd.ContributionOrder);
        }

        public IEnumerable<ClothingPiece> ReadClothingPiecesOfDesigner(int designerId){
            return _context.Designers
                .Include(d => d.ClothingPieces)
                .ThenInclude(cpd => cpd.ClothingPiece)
                .ThenInclude(cp => cp.Designers)
                .Single(d => d.Id == designerId)
                .ClothingPieces
                .Select(cpd => cpd.ClothingPiece);
        }

        public IEnumerable<Designer> ReadDesignersOfClothingPiece(int clothingPieceId){
            return _context.ClothingPieces
                .Include(cp => cp.Designers)
                .ThenInclude(cpd => cpd.Designer)
                .ThenInclude(d => d.ClothingPieces)
                .Single(cp => cp.Id == clothingPieceId)
                .Designers
                .Select(cpd => cpd.Designer);
        }

        public IEnumerable<Designer> ReadAvailableDesignersOfClothingPiece(int clothingPieceId){
            return _context.Designers
                .Except(
                    _context.ClothingPieceDesigners
                        .Include(cpd => cpd.Designer)
                        .Include(cpd => cpd.ClothingPiece)
                        .Where(cpd => cpd.ClothingPiece.Id == clothingPieceId)
                        .Distinct()
                        .Select(cpd => cpd.Designer)
                );
        }

        public ClothingPieceDesigner CreateClothingPieceDesigner(ClothingPieceDesigner clothingPieceDesigner){
            _context.ClothingPieceDesigners.Add(clothingPieceDesigner);
            _context.SaveChanges();
            return clothingPieceDesigner;
        }

        public void DeleteClothingPieceDesigner(int clothingPieceId, int designerId){
            _context.ClothingPieceDesigners.Remove(
                _context.ClothingPieceDesigners
                    .Where(cpd => cpd.ClothingPiece.Id == clothingPieceId)
                    .First(cpd => cpd.Designer.Id == designerId));
            _context.SaveChanges();
        }
    }
}