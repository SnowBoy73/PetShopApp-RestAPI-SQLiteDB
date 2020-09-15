using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetTypeRepository: IPetTypeRepository
    {
        static int id = 1; // as DB cannot have an id of 0
        private static List<PetType> _petTypes = new List<PetType>();

      
        public PetTypeRepository()
        {
           
        }


        public PetType CreatePetType(PetType petType)
        {
        petType.PetTypeId = id++;
            _petTypes.Add(petType);
            return petType;
        }



        public IEnumerable<PetType> ReadAllPetTypes()
        {
            return _petTypes;
        }



        public PetType ReadById(int id)
        {
            foreach (var petType in _petTypes)
            {
                if (petType.PetTypeId == id)
                {
                    return petType;
                }
            }
            return null;
        }



        // Remove later for UOW
        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            var petTypeFromDB = this.ReadById(petTypeUpdate.PetTypeId);
            if (petTypeFromDB != null)
            {
            petTypeFromDB.Name = petTypeUpdate.Name;
                   
                return petTypeFromDB;
            }
            return null;
        }


        public PetType DeletePetType(int id)
        {
            var petTypeFound = this.ReadById(id);
            if (petTypeFound != null)
            {
                _petTypes.Remove(petTypeFound);
                return petTypeFound;
            }
            return null;
        }

    }
}
