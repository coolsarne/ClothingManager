using System.Collections.Generic;
using System.Linq;
using ClothingManager.BL.Domain;
using ClothingManager.DAL;
using HotChocolate;

namespace GraphQlService.GraphQL;

public class Query
{
    public Designer GetDesigner([Service] IRepository repository, int id)
    {
        return repository.ReadDesigner(id);
    }
    
    public List<Designer> GetDesigners([Service] IRepository repository,string nationality = null)
    {
        if(string.IsNullOrWhiteSpace(nationality)) return repository.ReadAllDesigners().ToList();
        return repository.FilterByNationality(nationality);
    }

    public ClothingPiece GetClothingPiece([Service] IRepository repository, int id)
    {
        return repository.ReadClothingPiece(id);
    }

    public List<ClothingPiece> GetClothingPieces([Service] IRepository repository)
    {
        return repository.ReadAllClothingPieces().ToList();
    }

    public List<Designer> FilterByNationality([Service] IRepository repository, string nationality)
    {
        return repository.FilterByNationality(nationality);
    }

}