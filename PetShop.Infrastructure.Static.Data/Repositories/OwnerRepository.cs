using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Linq;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        readonly PetShopContext _ctx;


        public OwnerRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }


        public Owner CreateOwner(Owner owner)
        {
            Owner o = _ctx.Owners.Add(owner).Entity;
            _ctx.SaveChanges();
            return o;
        }



        public IEnumerable<Owner> ReadAllOwners()
         {
            return _ctx.Owners;
        }



        public Owner ReadById(int id)
        {
            return _ctx.Owners.FirstOrDefault(p => p.OwnerId == id);
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
        //        _owners.Remove(ownerFound);
                return ownerFound;
            }
            return null;
        }

    }
}
