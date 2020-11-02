using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        readonly PetShopContext _ctx;


        public PetRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }


        public Pet CreatePet(Pet pet)
        {
            /*    if (pet.PreviousOwner != null &&
                    _ctx.ChangeTracker.Entries<Owner>()
                    .FirstOrDefault(ce => ce.Entity.OwnerId == pet.PreviousOwner.OwnerId) == null)
                {
                    _ctx.Attach(pet.PreviousOwner);
                }
                Pet p = _ctx.Pets.Add(pet).Entity;
                _ctx.SaveChanges();
                return p;  */
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();
            return pet;
        }



        public IEnumerable<Pet> ReadAllPets()
        {
            return _ctx.Pets.Include(p => p.type); // 201030
        }



        public Pet ReadById(int id)
        {
            // return _ctx.Pets.FirstOrDefault(p => p.PetId == id);
            //  return _ctx.Pets.Include(p => p.PetsOwner).FirstOrDefault(p => p.PetId == id);

             var pet = _ctx.Pets.Include(p => p.petOwner).FirstOrDefault(p => p.petId == id);  // (p => p.PetType);
            //var pet = _ctx.Pets.Include(p => p.Type).FirstOrDefault(p => p.PetId == id).Include(p => p.PetOwner);
            return pet;
        }



        // Remove later for UOW
        public Pet UpdatePet(Pet petUpdate)
        {
            /*   var petFromDB = this.ReadById(petUpdate.PetId);
               if (petFromDB != null)
               {
                   petFromDB.Name = petUpdate.Name;
                   petFromDB.Type = petUpdate.Type;
                   petFromDB.BirthDate = petUpdate.BirthDate;
                   petFromDB.SoldDate = petUpdate.SoldDate;
                   petFromDB.Colour = petUpdate.Colour;
                   petFromDB.PreviousOwner = petUpdate.PreviousOwner;
                   petFromDB.Price = petUpdate.Price;
                   return petFromDB;
               }
               return null;    */

            /*      if (petUpdate.PreviousOwner != null &&
                         _ctx.ChangeTracker.Entries<Owner>()
                         .FirstOrDefault(ce => ce.Entity.OwnerId == petUpdate.PreviousOwner.OwnerId) == null)
                  {
                      _ctx.Attach(petUpdate.PreviousOwner);
                  }
                  else
                  {
                      _ctx.Entry(petUpdate).Reference(p => p.PreviousOwner).IsModified = true;
                  }
                  var updatedPet = _ctx.Update(petUpdate).Entity;
                  _ctx.SaveChanges();  */

            _ctx.Attach(petUpdate).State = EntityState.Modified;
            _ctx.Entry(petUpdate).Reference(p => p.petOwner).IsModified = true;
            _ctx.SaveChanges();

            //return updatedPet;
            return petUpdate;
        }



        public Pet DeletePet(int id)
        {
            var petRemoved = _ctx.Remove(new Pet { petId = id }).Entity;
            _ctx.SaveChanges();
            return petRemoved;
        }
    }

}
