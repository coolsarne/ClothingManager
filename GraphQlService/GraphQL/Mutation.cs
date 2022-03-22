using System;
using System.Collections.Generic;
using ClothingManager.BL.Domain;
using ClothingManager.DAL;
using HotChocolate;

namespace GraphQlService.GraphQL;

public class Mutation
{
    public Designer CreateDesigner([Service] IRepository repository, string name, int age, string nationality)
    {
        var createdDesigner = repository.CreateDesigner(new Designer()
        {
            Name = name,
            Age = age,
            Nationality = nationality,
            ClothingPieces = new List<ClothingPieceDesigner>()
        });
        return createdDesigner;
    }
public ClothingPiece CreateClothingPiece([Service] IRepository repository, double? price, DateTime manufactureDate,
        string color, ClothingType clothingType)
    {
        var createdClothingPiece = repository.CreateClothingPiece(new ClothingPiece()
        {
            Price = price,
            ManufactureDate = manufactureDate,
            Color = color,
            ClothingType = clothingType,
            Designers = new List<ClothingPieceDesigner>()
        });
        return createdClothingPiece;
    }
    

    public Designer RemoveDesigner([Service] IRepository repository, Designer designer)
    {
        return repository.DeleteDesigner(designer);
    }

    public ClothingPiece RemoveClothingPiece([Service] IRepository repository, ClothingPiece clothingPiece)
    {
        return repository.DeleteClothingPiece(clothingPiece);
    }
}