using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetTypeRepository: IPetTypeRepository
    {
        readonly PetShopContext _ctx;


        public PetTypeRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }


        public PetType CreatePetType(PetType petType)
        {
            PetType pt = _ctx.PetTypes.Add(petType).Entity;
            _ctx.SaveChanges();
            return pt;
        }



        public IEnumerable<PetType> ReadAllPetTypes()
        {
            return _ctx.PetTypes;
        }



        public PetType ReadById(int id)
        {
            return _ctx.PetTypes.FirstOrDefault(p => p.petTypeId == id);
        }



        // Remove later for UOW
        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            var petTypeFromDB = this.ReadById(petTypeUpdate.petTypeId);
            if (petTypeFromDB != null)
            {
            petTypeFromDB.name = petTypeUpdate.name;
                   
                return petTypeFromDB;
            }
            return null;
        }


        public PetType DeletePetType(int id)
        {
            var petTypeRemoved = _ctx.Remove(new PetType { petTypeId = id }).Entity;
            _ctx.SaveChanges();
            return petTypeRemoved;
        }

    }
}
