using System.Text;
using ClothingManager.BL.Domain;

namespace ClothingManager.UI.CA{
    public static class DesignerExtensions{
        internal static string ToStringExtended(this Designer designer){
            if (designer.ClothingPieces != null){
                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("{0} (age: {1})", designer.Name, designer.Age));
                foreach (ClothingPieceDesigner clothingPieceDesigner in designer.ClothingPieces){
                    if (clothingPieceDesigner != null){
                        sb.Append(string.Format("\n\t{0}", clothingPieceDesigner.ClothingPiece.ToStringExtended()));
                    }
                }

                return sb.ToString();
            }
            else{
                return string.Format("{0} (age: {1})", designer.Name, designer.Age);
            }
        }
    }
}