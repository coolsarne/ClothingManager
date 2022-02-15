using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClothingManager.BL;
using ClothingManager.DAL;
using ClothingManager.DAL.EF;
using ClothingManager.BL.Domain;

namespace ClothingManager.UI.CA{
    class Program{
        private readonly IManager _manager;

        private Program(IManager manager){
            _manager = manager;
        }

        static void Main(string[] args){
            // IRepository repository = new InMemoryRepository();
            ClothingManagerDbContext context = new ClothingManagerDbContext();
            IRepository repository = new Repository(context);
            IManager manager = new Manager(repository);
            Program program = new Program(manager);
            program.Run();
        }

        void Run(){
            bool stopLoop = false;
            do{
                ShowMenu();
                int input = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("==========================\n");
                switch (input){
                    case 0:
                        stopLoop = true;
                        break;
                    case 1:
                        ShowAllClothingPieces();
                        break;
                    case 2:
                        ShowAllDesigners();
                        break;
                    case 3:
                        ShowClothingPiecesOfType();
                        break;
                    case 4:
                        ShowDesignersByNameAndAge();
                        break;
                    case 5:
                        AddClothingPiece();
                        break;
                    case 6:
                        AddDesigner();
                        break;
                    case 7:
                        AddClothingPieceToDesigner();
                        break;
                    case 8:
                        RemoveClothingPieceFromDesigner();
                        break;
                    case 9:
                        ShowTest();
                        break;
                    default:
                        Console.WriteLine(input + " is not a valid number\n");
                        break;
                }
            } while (stopLoop == false);
        }

        private void ShowTest(){
            Store store1 = _manager.GetStoreWithClothingPieces(2);
            foreach (ClothingPiece clothingPiece in store1.ClothingPieces){
                Console.WriteLine(clothingPiece.ToStringExtended());
            }
            Console.WriteLine("Test passed\n================================\n");
            foreach (var store in _manager.GetAllStoresWithClothingPieces()){
                foreach (var clothingPiece in store.ClothingPieces){
                    Console.WriteLine(clothingPiece.ToStringExtended());
                }
                Console.WriteLine("STORE BREAK JOEHOE");
            }

        }

        private void ShowMenu(){
            Console.WriteLine("\nWhat would you like to do?\n==========================");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Show all Clothing pieces");
            Console.WriteLine("2) Show all Designers");
            Console.WriteLine("3) Show Clothing pieces of type");
            Console.WriteLine("4) Show Designers with name and/or age");
            Console.WriteLine("5) Add a Clothing piece");
            Console.WriteLine("6) Add a Designer");
            Console.WriteLine("7) Add Clothing Piece to Designer");
            Console.WriteLine("8) Remove Clothing Piece from Designer");
            Console.Write("Choice (0-8): ");
        }

        private void AddDesigner(){
            bool designerValid;
            do{
                try{
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Age: ");
                    int age = int.TryParse(Console.ReadLine(), out int k) ? k : new int();
                    Console.Write("Nationality: ");
                    string nationality = Console.ReadLine();
                    _manager.AddDesigner(name, age, nationality);
                    designerValid = true;
                    Console.WriteLine("\nSuccesfully added designer!");
                }
                catch (ValidationException validationException){
                    Console.WriteLine(validationException.ValidationResult.ErrorMessage);
                    designerValid = false;
                }
            } while (!designerValid);
        }

        private void AddClothingPiece(){
            bool clothingPieceValid;
            do{
                try{
                    bool accepted = false;
                    double? price;
                    do{
                        Console.Write("Price: ");
                        price = double.TryParse(Console.ReadLine(), out double d) ? d : null;
                        if (price >= 1000.00){
                            Console.Write(
                                $"The price you filled in seems to be a high amount: ({price:0.00 EUR})\nIs it intended to be this high? (y/n): ");
                            string check = Console.ReadLine();
                            accepted = check.ToLower().StartsWith('y');
                            if (!accepted) Console.WriteLine("\nYou might want to use \".\" as comma seperator");
                        }
                        else if (price == null){
                            Console.Write(
                                $"The price you filled in seems to be empty, this means the product will be listed as \"FREE\"\nIs it intended to be free? (y/n): ");
                            string check = Console.ReadLine();
                            accepted = check.ToLower().StartsWith('y');
                            Console.WriteLine();
                        }
                        else accepted = true;
                    } while (!accepted);

                    Console.Write("Manufacturedate (dd/mm/yyyy): ");
                    string dateinput = Console.ReadLine();
                    DateTime manufactureDate;
                    DateTime.TryParse(dateinput, out manufactureDate);
                    Console.Write("Color: ");
                    string color = Console.ReadLine();
                    Console.Write("Clothing Type: (");
                    foreach (var clothingType in Enum.GetValues(typeof(ClothingType))){
                        Console.Write("{0:d}={0}, ", clothingType);
                    }

                    Console.Write("\b\b): ");
                    int clothingTypeInput = int.TryParse(Console.ReadLine(), out int j) ? j : new int();
                    int clothingTypeEnumLength = Enum.GetNames(typeof(ClothingType)).Length;
                    if (clothingTypeInput <= 0 || clothingTypeInput > clothingTypeEnumLength)
                        throw new ValidationException("Clothing type must be value between 1 and " +
                                                      clothingTypeEnumLength);
                    ClothingType clothingTypeArg = (ClothingType) clothingTypeInput;
                    _manager.AddClothingPiece(price, manufactureDate, color, clothingTypeArg);
                    clothingPieceValid = true;
                    Console.WriteLine("\nSuccesfully added Clothing piece!");
                }
                catch (ValidationException validationException){
                    Console.WriteLine(validationException.ValidationResult.ErrorMessage);
                    clothingPieceValid = false;
                }
            } while (!clothingPieceValid);
        }

        private void ShowDesignersByNameAndAge(){
            Console.Write("Enter (part of) a name or leave blank: ");
            string inputName = Console.ReadLine();
            Console.Write("Enter the exact age of designer or leave blank: ");
            int? inputAge = int.TryParse(Console.ReadLine(), out int i) ? i : new int?();
            Console.WriteLine();
            foreach (var designer in _manager.GetDesignerByNameAndAge(inputName, inputAge)){
                Console.WriteLine(designer.ToStringExtended());
            }

            Console.Write("\n==========================");
        }

        private void ShowClothingPiecesOfType(){
            Console.Write("Clothing Type: (");
            foreach (var clothingType in Enum.GetValues(typeof(ClothingType))){
                Console.Write("{0:d}={0}, ", clothingType);
            }

            Console.Write("\b\b): ");
            int inputClothingType = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("==========================\n");
            ClothingType filter = (ClothingType) inputClothingType;
            foreach (var clothingPiece in _manager.GetClothingPiecesOfType(filter)){
                Console.WriteLine(clothingPiece.ToStringExtended());
            }

            Console.Write("\n==========================");
        }

        private void ShowAllDesigners(){
            foreach (var designer in _manager.GetAllDesignersWithClothingPiecesAndStore()){
                Console.WriteLine(designer.ToStringExtended());
            }

            Console.Write("\n==========================");
        }

        private void ShowAllClothingPieces(){
            foreach (var clothingPiece in _manager.GetAllClothingPiecesWithStore()){
                Console.WriteLine(clothingPiece.ToStringExtended());
            }

            Console.Write("\n==========================");
        }
        
        private void AddClothingPieceToDesigner(){
            Console.WriteLine("Which Designer would you like to add a Clothing Piece to?");
            var allDesigners = _manager.GetAllDesigners();
            foreach (var designer in allDesigners){
                Console.WriteLine(string.Format("[{0}] {1}", designer.Id, designer.Name));
            }
            Console.Write("Please enter a Designer ID: ");
            int designerInput = Convert.ToInt32(Console.ReadLine());
            Designer chosenDesigner = allDesigners.Single(d => d.Id == designerInput);
            Console.WriteLine("\nWhich Clothing Piece would you like to Assign to " + chosenDesigner.Name + "?");
            var allClothingPieces = _manager.GetAllClothingPieces();
            foreach (var clothingPiece in allClothingPieces){
                Console.WriteLine(string.Format("[{0}] {1}", clothingPiece.Id, clothingPiece.ToStringNameOnly()));
            }
            Console.Write("Please enter a Clothing Piece ID: ");
            int clothingPieceInput = Convert.ToInt32(Console.ReadLine());
            ClothingPiece chosenClothingPiece = allClothingPieces.Single(cp => cp.Id == clothingPieceInput);
            _manager.AddClothingPieceDesigner(chosenClothingPiece, chosenDesigner);
            Console.WriteLine("Succesfully added [" + chosenClothingPiece.ToStringNameOnly() + "] to [" + chosenDesigner.Name + "]");
        }
        
        private void RemoveClothingPieceFromDesigner(){
            Console.WriteLine("Which Designer would you like to remove a Clothing Piece from?");
            var allDesigners = _manager.GetAllDesigners();
            foreach (var designer in allDesigners){
                Console.WriteLine(string.Format("[{0}] {1}", designer.Id, designer.Name));
            }
            Console.Write("Please enter a Designer ID: ");
            int designerInput = Convert.ToInt32(Console.ReadLine());
            Designer chosenDesigner = allDesigners.Single(d => d.Id == designerInput);
            Console.WriteLine("\nWhich Clothing Piece would you like to remove from " + chosenDesigner.Name + "?");
            var allClothingPiecesOfDesigner = _manager.GetClothingPiecesOfDesigner(chosenDesigner.Id);
            foreach (var clothingPiece in allClothingPiecesOfDesigner){
                Console.WriteLine(string.Format("[{0}] {1}", clothingPiece.Id, clothingPiece.ToStringNameOnly()));
            }
            Console.Write("Please enter a Clothing Piece ID: ");
            int clothingPieceInput = Convert.ToInt32(Console.ReadLine());
            ClothingPiece chosenClothingPiece = allClothingPiecesOfDesigner.Single(cp => cp.Id == clothingPieceInput);
            Console.WriteLine(string.Format("designer id: {0}, clothing piece id: {1}", chosenDesigner.Id, chosenClothingPiece.Id));
            _manager.RemoveClothingPieceDesigner(chosenClothingPiece.Id, chosenDesigner.Id);
            Console.WriteLine("Succesfully removed [" + chosenClothingPiece.ToStringNameOnly() + "] from [" + chosenDesigner.Name + "]");
        }
        
    }
}