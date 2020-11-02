using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{
    public interface IPetTypeService
    {
        // New PetType
        //PetType NewPetType(string type);

        // Create
        PetType CreatePetType(PetType createdPetType);

        // Read
        PetType FindPetTypeById(int id);
        List<PetType> GetAllPetTypes();
        List<PetType> FindPetTypesByProperty(Filter filter);

        // Update
        PetType UpdatePetType(PetType petTypeUpdate);

        // Delete
        PetType DeletePetType(int id);

    }
}
