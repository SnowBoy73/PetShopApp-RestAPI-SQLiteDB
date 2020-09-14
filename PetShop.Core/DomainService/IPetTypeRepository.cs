using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainService
{
    public interface IPetTypeRepository
    {
        // Create PetType
        // No id when enter, id when exit
        PetType CreatePetType(PetType petType);

        // Read PetType(s)
        PetType ReadById(int id);
        IEnumerable<PetType> ReadAllPetTypes();

        // Update PetType
        PetType UpdatePetType(PetType petTypeUpdate);

        // Delete PetType
        PetType DeletePetType(int id);

    }
}
