using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {

        // Create Owner
        // No id when enter, id when exit
        Owner CreateOwner(Owner owner);

        // Read Owner(s)
        Owner ReadById(int id);
        IEnumerable<Owner> ReadAllOwners();
        Owner ReadByIdIncludingPets(int id);

        // Update Owner
        Owner UpdateOwner(Owner ownerUpdate);

        // Delete Owner
        Owner DeleteOwner(int id);

        
    }
}
