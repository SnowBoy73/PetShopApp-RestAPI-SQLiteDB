using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        // New Owner
        Owner NewOwner(string name, string address, List<Pet> petsOwned);

        // Create
        Owner CreateOwner(Owner createdOwner);

        // Read
        Owner FindOwnerById(int id);
        Owner FindOwnerByIdIncludingPets(int id);
        List<Owner> GetAllOwners();
        List<Owner> FindOwnersByProperty(Filter filter);

        // Update
        Owner UpdateOwner(Owner ownerUpdate);

        // Delete
        Owner DeleteOwner(int id);


    }
}
