using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.SQL.Repositories
{
    public class PetSQLRepository: IPetRepository
        
    {
       private PetShopDBContext _ctx;  // readonly??


        public PetSQLRepository(PetShopDBContext ctx)
        {
            _ctx = ctx;
        }



        public Pet CreatePet(Pet pet)
        {
            /*       var petEntry = _ctx.Add(pet);
                   _ctx.SaveChanges();
                   return petEntry.Entity;
            */
            return null;
        }



        public Pet DeletePet(int id)
        {
            //  var petEntry = _ctx.Remove
            return null;
        }



        public IEnumerable<Pet> ReadAllPets()   // List
        {
            /*       var petList = new List<Pet>();
                   petList.TotalCount = _ctx.Pets.Count();
                   petList.FilterUsed = filter;
                   petList.List = _ctx.Pets.ToList();
                   return petList;
            */
            return null;

        }



        public Pet ReadById(int id)
        {
            /*       return _ctx.Pet.FirstOrDefault(p Pet => p.Id == id);
                   .Include(navigationPropertyPath: p: Pet => p.Owner)   // join tables
                       .ThenInclude( o: Owner)
                       FirstOrDefault
            */
            return null;
        }



        public Pet UpdatePet(Pet petUpdate)
        {
            /*         var petEntry = _ctx.Update(petUpdate);
                     _ctx.SaveChanges();
                     return petEntry.Entity;  // ??
            */
            return null;

        }

    }
}
