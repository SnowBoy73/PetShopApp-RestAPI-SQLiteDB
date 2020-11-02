using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        // New Pet
        Pet NewPet(string name, PetType type, string colour, DateTime birthDate, double price, DateTime soldDate, Owner previousOwner);

        // Create
        Pet CreatePet(Pet createdPet);

        // Read
        Pet FindPetById(int id);
        IEnumerable<Pet> GetAllPets();
        List<Pet> FindPetsByProperty(Filter filter);

        // Update
        Pet UpdatePet(Pet petUpdate);
       
        // Delete
        Pet DeletePet(int id);


    }
}
