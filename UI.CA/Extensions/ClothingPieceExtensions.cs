using System;
using ClothingManager.BL.Domain;

namespace ClothingManager.UI.CA{
    public static class ClothingPieceExtensions{
        internal static string ToStringExtended(this ClothingPiece clothingPiece){
            if (clothingPiece.Store is not null){
                return string.Format("{0} {1} (Price: {2:0.00 EUR;;Free!}) from {3} {4:yyyy} collection, for sale in: {5}.", clothingPiece.Color,clothingPiece.ClothingType, clothingPiece.Price  ?? 0.00,GetManufactureDateSeason(clothingPiece.ManufactureDate),clothingPiece.ManufactureDate, clothingPiece.Store.ToStringExtended());
            }
            else{
                return string.Format("{0} {1} (Price: {2:0.00 EUR;;Free!}) from {3} {4:yyyy} collection.", clothingPiece.Color,clothingPiece.ClothingType, clothingPiece.Price ?? 0.00,GetManufactureDateSeason(clothingPiece.ManufactureDate),clothingPiece.ManufactureDate);
            }
        }

        private static string GetManufactureDateSeason(DateTime manufactureDate){
            float dateValue = (float)manufactureDate.Month + manufactureDate.Day / 100f;  // month.day (ex: 5 February = 2.05)
            if (dateValue < 3.21 || dateValue >= 12.22) return "Winter";
            if (dateValue < 6.21) return "Spring";
            if (dateValue < 9.23) return "Summer";
            return "Autumn";
        }

        internal static string ToStringNameOnly(this ClothingPiece clothingPiece){
            return
                $"{clothingPiece.Color} {clothingPiece.ClothingType} (Price: {clothingPiece.Price ?? 0.00:0.00 EUR;;Free!}) from {GetManufactureDateSeason(clothingPiece.ManufactureDate)} {clothingPiece.ManufactureDate:yyyy}";
        }
        
        
    }
}