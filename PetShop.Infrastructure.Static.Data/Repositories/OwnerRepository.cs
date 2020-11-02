 using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return _ctx.Owners.FirstOrDefault(o => o.ownerId == id);
        }



        public Owner ReadByIdIncludingPets(int id)
        {
            return _ctx.Owners.Include(o => o.petsOwned).FirstOrDefault(o => o.ownerId == id);
        }



        // Remove later for UOW
        public Owner UpdateOwner(Owner ownerUpdate)
        {
            var ownerFromDB = this.ReadById(ownerUpdate.ownerId);
            if (ownerFromDB != null)
            {
                ownerFromDB.name = ownerUpdate.name;
                ownerFromDB.address = ownerUpdate.address;
                ownerFromDB.petsOwned = ownerUpdate.petsOwned;
                return ownerFromDB;
            }
            return null;
        }


        public Owner DeleteOwner(int id)
        {
          //  var petsToRemove = _ctx.Pets.Where(p => p.PetsOwner.OwnerId == id);
          //  _ctx.RemoveRange(petsToRemove);
            var ownerRemoved = _ctx.Remove(new Owner { ownerId = id }).Entity;
            _ctx.SaveChanges();
            return ownerRemoved;
        }

       
    }
}
