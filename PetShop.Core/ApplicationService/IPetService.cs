using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        // New Pet
        Pet NewPet(string name, string type, /*gender*/ string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner);

        // Create
        Pet CreatePet(Pet createdPet);

        // Read
        Pet FindPetById(int id);
        List<Pet> GetAllPets();
        List<Pet> FindPetsByProperty(string prop, string searchValue);

        // Update
        Pet UpdatePet(Pet petUpdate);
       
        // Delete
        Pet DeletePet(int id);


    }
}
