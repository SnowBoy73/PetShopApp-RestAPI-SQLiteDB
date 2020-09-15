using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService.Impl
{
    public class OwnerService : IOwnerService
    {
        readonly IOwnerRepository _ownerRepo;
        readonly IPetRepository _petRepo;

        public OwnerService(IOwnerRepository ownerRepository, IPetRepository petRepository)
        {
            _ownerRepo = ownerRepository;
            _petRepo = petRepository;
        }


        public Owner NewOwner(string name, string address, List<Pet> petsOwned)
        {
            var newOwner = new Owner()
            {
                Name = name,
                Address = address,
                PetsOwned = petsOwned
            };
            return newOwner;
        }


        public Owner CreateOwner(Owner createdOwner)
        {
            return _ownerRepo.CreateOwner(createdOwner);
        }


        public Owner FindOwnerById(int id)
        {
            return _ownerRepo.ReadById(id);
        }


        public Owner FindOwnerByIdIncludingPets(int id)
        {
            var owner = _ownerRepo.ReadById(id);
            if (owner == null)
            {
                return null ;
            }
            owner.PetsOwned = _petRepo.ReadAllPets().Where(pet => pet.PreviousOwner.OwnerId == owner.OwnerId).ToList();
            return owner;
        }


        public List<Owner> FindOwnerByProperty(string prop, string searchValue)
        {
            IEnumerable<Owner> query;
            var list = _ownerRepo.ReadAllOwners();
            switch (prop)
            {
                case "1":
                    query = list.Where(owner => owner.Name.ToLower().Contains(searchValue.ToLower()));
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
                default:
                    Console.WriteLine("That is not a valid property");  // Shouldn't happen, alresdy exceptioned
                    break;
            }
            return null;
        }



        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.ReadAllOwners().ToList();
        }



        public Owner UpdateOwner(Owner ownerUpdate)
        {
            var updatedOwner = FindOwnerById(ownerUpdate.OwnerId); //FindOwnerById(ownerUpdate.OwnerId);
            if (updatedOwner == null)
            {
                return null;
            }
            else
            {
                updatedOwner.Name = ownerUpdate.Name;
                updatedOwner.Address = ownerUpdate.Address;
                updatedOwner.PetsOwned = ownerUpdate.PetsOwned;
                return updatedOwner;
            }
        }



        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.DeleteOwner(id);
        }

    }
}
