using ClothingManager.BL.Domain;

namespace ClothingManager.UI.CA{
    public static class StoreExtensions{
        internal static string ToStringExtended(this Store store){
            return string.Format("\"{0}\", located in: {1} {2}", store.Name, store.Zipcode, store.City);
        }
    }
}