using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetTypeService: IPetTypeService
    {
        readonly IPetTypeRepository _petTypeRepo;

        public PetTypeService(IPetTypeRepository petTypeRepository, IPetRepository petRepository)
        {
            _petTypeRepo = petTypeRepository;
        }
           

        public PetType NewPetType(string type)
        {
            var newPetType = new PetType()
            {
                Name = type,
            };
            return newPetType;
        }


        public PetType CreatePetType(PetType createdPetType)
        {
            return _petTypeRepo.CreatePetType(createdPetType);
        }


        public PetType FindPetTypeById(int id)
        {
            return _petTypeRepo.ReadById(id);
        }



         public List<PetType> FindPetTypesByProperty(Filter filter)
        {
            IEnumerable<PetType> results;
            var list = _petTypeRepo.ReadAllPetTypes();
            switch (filter.Property)
            {
                case "name":
                    results = list.Where(owner => owner.Name.ToLower().Contains(filter.Value));
                    return results.ToList();
            }
            return null;   // Should never happen
        }



        public List<PetType> GetAllPetTypes()
        {
            return _petTypeRepo.ReadAllPetTypes().ToList();
        }



        public PetType UpdatePetType(PetType petTypeUpdate)
        {
            var updatedPetType = FindPetTypeById(petTypeUpdate.PetTypeId); //FindOwnerById(ownerUpdate.OwnerId);
            if (updatedPetType == null)
            {
                return null;
            }
            else
            {
            updatedPetType.Name = petTypeUpdate.Name;
                   
                return updatedPetType;
            }
        }



        public PetType DeletePetType(int id)
        {
            return _petTypeRepo.DeletePetType(id);
        }

    }
}
