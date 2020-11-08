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
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();
            return pet;
        }



        public IEnumerable<Pet> ReadAllPets()
        {
            return _ctx.Pets.Include(p => p.petOwner).Include(p => p.type); // 201106
        }



        public Pet ReadById(int id)
        {
            // var pet = _ctx.Pets.Include(p => p.petOwner).FirstOrDefault(p => p.petId == id);

            var pet = _ctx.Pets.Include(p => p.type).FirstOrDefault(p => p.petId == id);
           // && Pets.Include(p => p.type).FirstOrDefault(p => p.petId == id); */
            return pet;
        }



        // Remove later for UOW
        public Pet UpdatePet(Pet petUpdate)
        {
            _ctx.Attach(petUpdate).State = EntityState.Modified;
            _ctx.Entry(petUpdate).Reference(p => p.petOwner).IsModified = true;
            _ctx.SaveChanges();
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
