using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        // Create Pet
        // No id when enter, id when exit
        Pet CreatePet(Pet pet);

        // Read Pet(s)
        Pet ReadById(int id);
        IEnumerable<Pet> ReadAllPets();

        // Update Pet
        Pet UpdatePet(Pet petUpdate);

        // Delete Pet
        Pet DeletePet(int id);

    }
}
