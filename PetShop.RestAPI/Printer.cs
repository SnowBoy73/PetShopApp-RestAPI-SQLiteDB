using System;
using System.Collections.Generic;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

namespace PetShop.RestAPI

{
    public class Printer : IPrinter
    {
     /*   #region Repository area

        private IPetService _petService;
        #endregion


        public Printer(IPetService petService)
        {
            _petService = petService;
        }



        #region UI

        public void StartUI()
        {
            string[] menuItems =
            {
                "List All Pets",
                "Search Pets by Type",
                "Add New Pet",
                "Update Pet",
                "Delete Pet ",
                "Exit"
            };

            var selection = ShowMenu(menuItems);
            var menuLength = menuItems.Length;

            while (selection != menuLength)
            {
                switch (selection)
                {
                    case 1:
                        var allPetsL = _petService.GetAllPets();
                        ListPets(allPetsL, allPetsL.Count);
                        Pause();
                        break;

                    case 2:
                        bool validSearchOption = false;
                        while (!validSearchOption)
                        {
                            Console.Clear();
                            Console.WriteLine("Search Pets by Property");
                            Console.WriteLine("_______________________\n");
                            var property = AskQuestion("Which property do you wish to Search? (1 = Name, 2 = Type, 3 = Price)");

                            if (property == "3")
                            {
                                var search = AskQuestion($"Would you like to view the cheapest pets first? (Y/N)");
                                var listSizeSTR = AskQuestion($"How many pets do you want to see?");
                                int listSize;
                                int.TryParse(listSizeSTR, out listSize);
                                string order;
                                if (search == "Y" || search == "y")
                                {
                                    order = "cheapest";
                                }
                                else
                                {
                                    order = "most expensive";
                                }
                                Console.Clear();
                                Console.WriteLine($"Search Pets by Price, viewing the {order} first':");
                                Console.WriteLine("_______________________\n");
                                var petsByPropertySearch = _petService.FindPetsByProperty(property, search);
                                ListPets(petsByPropertySearch, listSize);
                                Pause();
                                validSearchOption = true;
                                break;
                            }
                            else
                            {
                                if (property == "1" || property == "2")
                                {
                                    string propertySTR = "";
                                    if (property == "1")
                                        propertySTR = "Name";
                                    if (property == "2")
                                        propertySTR = "Type";
                                    var search = AskQuestion($"Enter the search of the pets {propertySTR}?");
                                    Console.Clear();
                                    Console.WriteLine($"Search Pets by {property} for '{search}':");
                                    Console.WriteLine("_______________________\n");
                                    var petsByPropertySearch = _petService.FindPetsByProperty(property, search);
                                    ListPets(petsByPropertySearch, petsByPropertySearch.Count);

                                    Pause();
                                    validSearchOption = true;
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("This is not a valid search option - please try again");
                                    Pause();
                                }
                            }
                        }
                        break;


                    case 3:
                        Console.Clear();
                        Console.WriteLine("Create New Pet");
                        Console.WriteLine("______________\n");
                        var nameC = AskQuestion("Name?");
                        var typeC = AskQuestion("Type?");
                        //  var genderC = AskQuestion("Gender? (M/F/O?")");
                        var colourC = AskQuestion("Colour?");
                        DateTime birthDateC = DateTime.Now;
                        bool validBirthDateC = false;
                        while (!validBirthDateC)
                        {
                            var birthDateCSTR = AskQuestion("Birth Date? (dd-mm-yyyy)");
                            var birthDateCFIX = ConvertMMddyyyyToddMMyyyy(birthDateCSTR);
                            if (CheckValidDate(birthDateCFIX))
                            {
                                birthDateC = Convert.ToDateTime(birthDateCFIX);
                                validBirthDateC = true;
                            }
                        }
                        bool validPriceC = false;
                        double priceC = 0;
                        string priceCSTR = "test";
                        while (!validPriceC)
                        {
                            priceCSTR = AskQuestion("Price?");
                            if (CheckValidPrice(priceCSTR))
                            {
                                priceC = Convert.ToDouble(priceCSTR);
                                validPriceC = true;
                            }
                        }
                        DateTime soldDateC = DateTime.Now;
                        bool validSoldDateC = false;
                        while (!validSoldDateC)
                        {
                            var soldDateCSTR = AskQuestion("Date Sold? (dd-mm-yyyy)");
                            var soldDateCFIX = ConvertMMddyyyyToddMMyyyy(soldDateCSTR);
                            if (CheckValidDate(soldDateCFIX))
                            {
                                soldDateC = Convert.ToDateTime(soldDateCFIX);
                                validSoldDateC = true;
                            }
                        }
                        var previousOwnerC = AskQuestion("Previous Owner?");
                        var createdPet = _petService.NewPet(nameC, typeC, colourC, birthDateC, priceC, soldDateC, previousOwnerC);
                        _petService.CreatePet(createdPet);
                        Console.WriteLine("The pet has been created");
                        Pause();
                        break;

                    case 4:
                        int petToUpdateId = 0;
                        Pet petToUpdate = null;
                        var allPetsU = _petService.GetAllPets();
                        petToUpdateId = CheckValidId(allPetsU, "update");
                        petToUpdate = _petService.FindPetById(petToUpdateId);
                        ListPet(petToUpdateId);
                        Console.Clear();
                        var petNameAndTypeU = $"{petToUpdate.Name} the {petToUpdate.Type}";
                        ListPet(petToUpdateId);
                        Console.WriteLine($"\nUpdating {petNameAndTypeU} - pressing enter will preserve the original information\n");
                        var nameU = AskQuestion("New name?");
                        if (nameU == "")
                            nameU = petToUpdate.Name;
                        var typeU = AskQuestion("New type?");
                        if (typeU == "")
                            typeU = petToUpdate.Type;
                        /*  var genderU = AskQuestion("New gender? (M/F/O?")");
                        if (genderU == "")
                            genderU = petToUpdate.Gender; */
          /*              var colourU = AskQuestion("New colour?");
                        if (colourU == "")
                            colourU = petToUpdate.Colour;
                        DateTime birthDateU = DateTime.Now;
                        bool validBirthDateU = false;
                        while (!validBirthDateU)
                        {
                            var birthDateUSTR = AskQuestion("New birth Date? (dd-mm-yyyy)");
                            if (birthDateUSTR == "")
                            {
                                birthDateU = petToUpdate.BirthDate;
                                validBirthDateU = true;
                            }
                            else
                            {
                                var birthDateUFIX = ConvertMMddyyyyToddMMyyyy(birthDateUSTR);
                                if (CheckValidDate(birthDateUFIX))
                                {
                                    birthDateU = Convert.ToDateTime(birthDateUFIX);
                                    validBirthDateU = true;
                                }
                            }
                        }
                        double priceU = 0;
                        string priceUSTR = "test";
                        bool validPriceU = false;
                        while (!validPriceU)
                        {
                            priceUSTR = AskQuestion("New Price?");
                            if (priceUSTR == "")
                            {
                                priceU = petToUpdate.Price;
                                validPriceU = true;
                            }
                            else
                            {
                                if (CheckValidPrice(priceUSTR))
                                {
                                    priceU = Convert.ToDouble(priceUSTR);
                                    validPriceU = true;
                                }
                            }
                        }
                        DateTime soldDateU = DateTime.Now;
                        bool validSoldDateU = false;
                        while (!validSoldDateU)
                        {
                            var soldDateUSTR = AskQuestion("New sold Date? (dd-mm-yyyy)");
                            if (soldDateUSTR == "")
                            {
                                soldDateU = petToUpdate.SoldDate;
                                validSoldDateU = true;
                            }
                            else
                            {
                                var soldDateUFIX = ConvertMMddyyyyToddMMyyyy(soldDateUSTR);
                                if (CheckValidDate(soldDateUFIX))
                                {
                                    soldDateU = Convert.ToDateTime(soldDateUFIX);
                                    validSoldDateU = true;
                                }
                            }
                        }
                        var previousOwnerU = AskQuestion("New previous owner?");
                        if (previousOwnerU == "")
                            previousOwnerU = petToUpdate.PreviousOwner;
                        var updatedPet = _petService.UpdatePet(new Pet()
                        {
                            PetId = petToUpdateId,
                            Name = nameU,
                            Type = typeU,
                            Colour = colourU,
                            BirthDate = birthDateU,
                            Price = priceU,
                            SoldDate = soldDateU,
                            PreviousOwner = previousOwnerU
                        });
                        _petService.UpdatePet(updatedPet);
                        Console.WriteLine($"{petNameAndTypeU} has been updated");
                        Pause();
                        break;

                    case 5:
                        int petToDeleteId = 0;
                        Pet petToDelete = null;
                        var allPetsD = _petService.GetAllPets();
                        petToDeleteId = CheckValidId(allPetsD, "delete");
                        petToDelete = _petService.FindPetById(petToDeleteId);
                        var petNameAndTypeD = $"{petToDelete.Name} the {petToDelete.Type}";
                        var deleteCheckQuestion = ($"Do you really want to delete {petNameAndTypeD} (Y/N)");
                        var deleteCheckAnswer = AskQuestion(deleteCheckQuestion);
                        if (deleteCheckAnswer == "Y" || deleteCheckAnswer == "y")
                        {
                            _petService.DeletePet(petToDeleteId);
                            Console.WriteLine($"{petNameAndTypeD} has been deleted");
                            Pause();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("No pet was deleted");
                            Pause();
                            break;
                        }

                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.Clear();
            Console.WriteLine("Thank you - please come again.");
            Console.ReadLine();
        }



        int CheckValidId(List<Pet> allPets, string feature)
        {
            bool validId = false;
            int petToUpdateId = 0;
            Pet petToUpdate = null;
            while (!validId)
            {
                ListPets(allPets, allPets.Count);
                petToUpdateId = PrintFindPetById($"\nWhich pet would like to {feature}? (enter id)");
                petToUpdate = _petService.FindPetById(petToUpdateId);
                if (petToUpdate == null)
                {
                    Console.WriteLine("That is not a valid pet id");
                    Pause();
                }
                else
                {
                    return petToUpdateId;
                }
            }
            return 0;
        }



        bool CheckValidPrice(string priceSTR)
        {
            double price;
            if (double.TryParse(priceSTR, out price) && price >= 0)
            {
                return true;
            }
            else
            {
                Console.WriteLine("that is not a valid price");
            }
            return false;
        }



        bool CheckValidDate(string dateValueSTR)
        {
            DateTime validDateValue;
            if (DateTime.TryParse(dateValueSTR, out validDateValue) && validDateValue < DateTime.Now && validDateValue > DateTime.Now.AddYears(-200))
            {
                return true;
            }
            else
            {
                Console.WriteLine("That is not a valid date - please try again");
                return false;
            }
        }



        string ConvertMMddyyyyToddMMyyyy(string input)
        {
            string mMddyyyy = "test";
            string[] splitDateSTR = input.Split("-");
            if (splitDateSTR.Length != 3)
            {
                return "FAIL";
            }
            mMddyyyy = ($"{splitDateSTR[1]}-{splitDateSTR[0]}-{splitDateSTR[2]}");
            return mMddyyyy;
        }



        int PrintFindPetById(string statement)
        {
            Console.WriteLine(statement);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("That is not a number - try again");
            }
            return (id - 1);
        }



        string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }



        void ListPet(int id)
        {
            var pet = _petService.FindPetById(id);
            Console.WriteLine($"Name: {pet.Name}  Type: {pet.Type}  Colour: {pet.Colour}  BirthDate: {pet.BirthDate.Day}-{pet.BirthDate.Month}-{pet.BirthDate.Year}  Price:{pet.Price}  Sold on: {pet.SoldDate.Day}-{pet.SoldDate.Month}-{pet.SoldDate.Year}  Previous Owner: {pet.PreviousOwner}");
            // Gender: {pet.Gender}

        }



        void ListPets(List<Pet> allPets, int listSize)
        {
            Console.Clear();
            Console.WriteLine("List of All Pets");
            Console.WriteLine("________________\n");
            int i = 0;
            if (allPets.Count == 0)
                Console.WriteLine("No pets found that match the search");
            foreach (var pet in allPets)
            {
                if (i >= listSize)
                    break;
                Console.WriteLine($"Id: {(pet.PetId + 1)} Name: {pet.Name}  Type: {pet.Type}  Colour: {pet.Colour}  BirthDate: {pet.BirthDate.Day}-{pet.BirthDate.Month}-{pet.BirthDate.Year}  Price:{pet.Price}  Sold on: {pet.SoldDate.Day}-{pet.SoldDate.Month}-{pet.SoldDate.Year}  Previous Owner: {pet.PreviousOwner}");

                // Gender: {pet.Gender}
                i++;
            }
        }



        int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Welcome to your Virtual Pet Shop");
            Console.WriteLine("________________________________\n");

            int NoOfItems = menuItems.Length;

            for (int i = 0; i < NoOfItems; i++)
            {
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }
            Console.WriteLine("");
            Console.WriteLine($"Please enter an option (1-{NoOfItems})");
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection) || selection < 1 || selection > NoOfItems)
            {
                Console.WriteLine($"That is not a valid option (1-{NoOfItems}) - please try again");
            }
            return selection;
        }



        public void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();
        }
        #endregion

        */
    }
}
