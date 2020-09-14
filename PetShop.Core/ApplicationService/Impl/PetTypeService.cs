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
                Type = type,
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


        /*    public PetType FindPetTypeByIdIncludingPets(int id)
         {
             var owner = _ownerRepo.ReadById(id);
             owner.PetsOwned = _petRepo.ReadAllPets().Where(pet => pet.PreviousOwner.OwnerId == owner.OwnerId).ToList();
             return owner;
         }
         */

         public List<PetType> FindPetTypesByProperty(string prop, string searchValue)
         {
            /*
            IEnumerable<PetType> query;
            var list = _petTypeRepo.ReadAllPetTypes();
            switch (prop)
            {
                case "1":
                    query = list.Where(PetType => PetType.Name.ToLower().Contains(searchValue.ToLower()));
                    return query.ToList(); ;

                case "2":
                    query = list.Where(owner => owner.Address.ToLower().Contains(searchValue.ToLower()));
                    return query.ToList(); ;

                case "3":
                /*   if (searchValue == "Y" || searchValue == "y")
                    query = list.OrderBy(pet => pet.Price);
                else
                    query = list.OrderBy(pet => pet.Price).Reverse();
                return query.ToList();
                */
            /*         default:
                        Console.WriteLine("That is not a valid property");  // Shouldn't happen, alresdy exceptioned
                        break;
                }
            */
                return null;
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
            updatedPetType.Type = petTypeUpdate.Type;
                   
                return updatedPetType;
            }
        }



        public PetType DeletePetType(int id)
        {
            return _petTypeRepo.DeletePetType(id);
        }

    }
}
