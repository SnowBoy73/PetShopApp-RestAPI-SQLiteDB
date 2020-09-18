using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Linq;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        static int id = 1; // as DB cannot have an id of 0
        private static List<Owner> _owners = new List<Owner>();

        public OwnerRepository()
        {
         
        }


        public Owner CreateOwner(Owner owner)
        {
            owner.OwnerId = id++;
            _owners.Add(owner);
            return owner;
        }



        public IEnumerable<Owner> ReadAllOwners()
         {
            return _owners;
        }



        public Owner ReadById(int id)
        {
            return _owners.Select(o => new Owner()
            {
                OwnerId = o.OwnerId,
                Name = o.Name,
                Address = o.Address
            }).FirstOrDefault(o => o.OwnerId == id);
        /*    foreach (var owner in _owners)
            {
                if (owner.OwnerId == id)
                {
                    return owner;
                }
            }
            return null;
        */
        }



        // Remove later for UOW
        public Owner UpdateOwner(Owner ownerUpdate)
        {
            var ownerFromDB = this.ReadById(ownerUpdate.OwnerId);
            if (ownerFromDB != null)
            {
                ownerFromDB.Name = ownerUpdate.Name;
                ownerFromDB.Address = ownerUpdate.Address;
                ownerFromDB.PetsOwned = ownerUpdate.PetsOwned;
                return ownerFromDB;
            }
            return null;
        }


        public Owner DeleteOwner(int id)
        {
            var ownerFound = this.ReadById(id);
            if (ownerFound != null)
            {
                _owners.Remove(ownerFound);
                return ownerFound;
            }
            return null;
        }

    }
}
