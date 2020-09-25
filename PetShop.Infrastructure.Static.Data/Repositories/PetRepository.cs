using System;
using System.Collections.Generic;
using System.Linq;
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
            Pet p = _ctx.Pets.Add(pet).Entity;
            _ctx.SaveChanges();
            return p;
        }



        public IEnumerable<Pet> ReadAllPets()
        {
            return _ctx.Pets;
        }



        public Pet ReadById(int id)
        {
            return _ctx.Pets.FirstOrDefault(p => p.PetId == id);
        }



        // Remove later for UOW
        public Pet UpdatePet(Pet petUpdate)
        {
            var petFromDB = this.ReadById(petUpdate.PetId);
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
            return null;
        }


        public Pet DeletePet(int id)
        {
            var petFound = this.ReadById(id);
            if (petFound != null)
            {
        //        _pets.Remove(petFound);
                return petFound;
            }
            return null;
        }

    }

}
