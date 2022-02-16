
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ClothingManager.BL.Domain;

namespace ClothingManager.DAL.EF{
    public static class ClothingManagerInitializer{
        public static bool IsDatabaseInitialized{ get; set; }

        public static void Initialize(ClothingManagerDbContext context, bool dropCreateDatabase = true){
            if (IsDatabaseInitialized){
                return;
            }
            
            if (dropCreateDatabase){
                context.Database.EnsureDeleted();
            }

            if (context.Database.EnsureCreated()){
                Seed(context);
            }
            
            IsDatabaseInitialized = true;

        }

        private static void Seed(ClothingManagerDbContext context){
            ClothingPiece piece1 = new ClothingPiece(79.99, new DateTime(2020, 12, 10), "black", ClothingType.Tshirt);
            ClothingPiece piece2 = new ClothingPiece(299.99, new DateTime(2021, 9, 1), "red", ClothingType.Jacket);
            ClothingPiece piece3 = new ClothingPiece(549.98, new DateTime(2021, 9, 21), "white", ClothingType.Shoes);
            ClothingPiece piece4 = new ClothingPiece(199.50, new DateTime(2021, 4, 6), "black", ClothingType.Sweater);
            
            ClothingPiece piece5 = new ClothingPiece(119.99, new DateTime(2021, 2, 10), "green", ClothingType.Sweater);
            ClothingPiece piece6 = new ClothingPiece(80.99, new DateTime(2021, 6, 26), "green", ClothingType.Tshirt);
            ClothingPiece piece7 = new ClothingPiece(499.99, new DateTime(2020, 12, 4), "red", ClothingType.Shoes);
            ClothingPiece piece8 = new ClothingPiece(649.50, new DateTime(2021, 5, 19), "grey", ClothingType.Jacket);
            ClothingPiece piece9 = new ClothingPiece(279.49, new DateTime(2021, 1, 7), "grey", ClothingType.Pants);
            ClothingPiece piece10 = new ClothingPiece(159.98, new DateTime(2019, 10, 12), "brown", ClothingType.Sweater);

            ClothingPiece piece11 = new ClothingPiece(200.19, new DateTime(2021, 11, 12), "brown", ClothingType.Sweater);
            ClothingPiece piece12 = new ClothingPiece(459.98, new DateTime(2022, 1, 4), "brown", ClothingType.Jacket);
            ClothingPiece piece13 = new ClothingPiece(49.98, new DateTime(2021, 6, 26), "brown", ClothingType.Tshirt);
            ClothingPiece piece14 = new ClothingPiece(219.49, new DateTime(2020, 12, 22), "brown", ClothingType.Shoes);
            ClothingPiece piece15 = new ClothingPiece(859.98, new DateTime(2022, 2, 1), "brown", ClothingType.Sweater);

            Designer designer1 = new Designer("Raf Simons", 53, "Belgian");
            Designer designer2 = new Designer("Ralph Lauren", 82, "American");
            Designer designer3 = new Designer("Louis Vuitton", 167, "French");
            Designer designer4 = new Designer("Coco Chanel", 87, "French");

            Designer designer5 = new Designer("Burberry", 165, "British");
            Designer designer6 = new Designer("Fred Perry", 85, "British");
            
            Store store1 = new Store("Paris", 75000, "Lorette & Jasmin", 1);
            Store store2 = new Store("Milan", 20019, "DMAG outlet", 2);

            Store store3 = new Store("Brussels", 1000, "Timeless", 3);
            Store store4 = new Store("Paris", 75000, "Pierre Paris Design", 4);
            Store store5 = new Store("London", 533420, "Matchesfashion", 5);
            
            ClothingPieceDesigner cpd1 = new ClothingPieceDesigner(){ClothingPiece = piece1, Designer = designer1, ContributionOrder = 1};
            ClothingPieceDesigner cpd2 = new ClothingPieceDesigner(){ClothingPiece = piece1, Designer = designer2, ContributionOrder = 2};
            ClothingPieceDesigner cpd3 = new ClothingPieceDesigner(){ClothingPiece = piece2, Designer = designer3, ContributionOrder = 1};
            ClothingPieceDesigner cpd4 = new ClothingPieceDesigner(){ClothingPiece = piece3, Designer = designer2, ContributionOrder = 1};
            ClothingPieceDesigner cpd5 = new ClothingPieceDesigner(){ClothingPiece = piece3, Designer = designer4, ContributionOrder = 2};
            ClothingPieceDesigner cpd6 = new ClothingPieceDesigner(){ClothingPiece = piece4, Designer = designer3, ContributionOrder = 1};

            ClothingPieceDesigner cpd7 = new ClothingPieceDesigner(){ClothingPiece = piece5, Designer = designer1, ContributionOrder = 1};
            ClothingPieceDesigner cpd8 = new ClothingPieceDesigner(){ClothingPiece = piece5, Designer = designer2, ContributionOrder = 2};
            ClothingPieceDesigner cpd9 = new ClothingPieceDesigner(){ClothingPiece = piece5, Designer = designer5, ContributionOrder = 3};
            ClothingPieceDesigner cpd10 = new ClothingPieceDesigner(){ClothingPiece = piece6, Designer = designer6, ContributionOrder = 1};
            ClothingPieceDesigner cpd11 = new ClothingPieceDesigner(){ClothingPiece = piece7, Designer = designer6, ContributionOrder = 1};
            ClothingPieceDesigner cpd12 = new ClothingPieceDesigner(){ClothingPiece = piece7, Designer = designer2, ContributionOrder = 2};
            ClothingPieceDesigner cpd13 = new ClothingPieceDesigner(){ClothingPiece = piece8, Designer = designer3, ContributionOrder = 1};
            ClothingPieceDesigner cpd14 = new ClothingPieceDesigner(){ClothingPiece = piece8, Designer = designer4, ContributionOrder = 2};
            ClothingPieceDesigner cpd15 = new ClothingPieceDesigner(){ClothingPiece = piece8, Designer = designer5, ContributionOrder = 3};
            ClothingPieceDesigner cpd16 = new ClothingPieceDesigner(){ClothingPiece = piece9, Designer = designer1, ContributionOrder = 1};
            ClothingPieceDesigner cpd17 = new ClothingPieceDesigner(){ClothingPiece = piece10, Designer = designer5, ContributionOrder = 1};
            ClothingPieceDesigner cpd18 = new ClothingPieceDesigner(){ClothingPiece = piece10, Designer = designer6, ContributionOrder = 2};
            
            piece1.Store = store1;
            piece2.Store = store1;
            piece3.Store = store2;
            piece4.Store = store2;
            
            piece5.Store = store5;
            piece6.Store = store4;
            piece7.Store = store3;
            piece8.Store = store4;
            piece9.Store = store3;
            piece10.Store = store2;
            
            store1.ClothingPieces = new List<ClothingPiece>(){piece1, piece2};
            store2.ClothingPieces = new List<ClothingPiece>(){piece3, piece4, piece10};

            store3.ClothingPieces = new List<ClothingPiece>(){piece7, piece9};
            store4.ClothingPieces = new List<ClothingPiece>(){piece6, piece8};
            store5.ClothingPieces = new List<ClothingPiece>(){piece5};
            
            context.ClothingPieces.Add(piece1);
            context.ClothingPieces.Add(piece2);
            context.ClothingPieces.Add(piece3);
            context.ClothingPieces.Add(piece4);
            context.ClothingPieces.Add(piece5);
            context.ClothingPieces.Add(piece6);
            context.ClothingPieces.Add(piece7);
            context.ClothingPieces.Add(piece8);
            context.ClothingPieces.Add(piece9);
            context.ClothingPieces.Add(piece10);
            context.ClothingPieces.Add(piece11);
            context.ClothingPieces.Add(piece12);
            context.ClothingPieces.Add(piece13);
            context.ClothingPieces.Add(piece14);
            context.ClothingPieces.Add(piece15);

            context.Designers.Add(designer1);
            context.Designers.Add(designer2);
            context.Designers.Add(designer3);
            context.Designers.Add(designer4);
            context.Designers.Add(designer5);
            context.Designers.Add(designer6);

            context.Stores.Add(store1);
            context.Stores.Add(store2);
            context.Stores.Add(store3);
            context.Stores.Add(store4);
            context.Stores.Add(store5);
            
            context.ClothingPieceDesigners.Add(cpd1);
            context.ClothingPieceDesigners.Add(cpd2);
            context.ClothingPieceDesigners.Add(cpd3);
            context.ClothingPieceDesigners.Add(cpd4);
            context.ClothingPieceDesigners.Add(cpd5);
            context.ClothingPieceDesigners.Add(cpd6);
            context.ClothingPieceDesigners.Add(cpd7);
            context.ClothingPieceDesigners.Add(cpd8);
            context.ClothingPieceDesigners.Add(cpd9);
            context.ClothingPieceDesigners.Add(cpd10);
            context.ClothingPieceDesigners.Add(cpd11);
            context.ClothingPieceDesigners.Add(cpd12);
            context.ClothingPieceDesigners.Add(cpd13);
            context.ClothingPieceDesigners.Add(cpd14);
            context.ClothingPieceDesigners.Add(cpd15);
            context.ClothingPieceDesigners.Add(cpd16);
            context.ClothingPieceDesigners.Add(cpd17);
            context.ClothingPieceDesigners.Add(cpd18);

            context.SaveChanges();
            context.ChangeTracker.Clear();
        }
        
    }
}