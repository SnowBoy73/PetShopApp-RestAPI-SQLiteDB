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
                name = name,
                address = address,
                petsOwned = petsOwned
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
            var owner = _ownerRepo.ReadByIdIncludingPets(id);
             return owner;
        }



        public List<Owner> FindOwnersByProperty(Filter filter)
        {
            IEnumerable<Owner> results;
            var list = _ownerRepo.ReadAllOwners();
            switch (filter.Property)
            {
                case "name":
                    results = list.Where(owner => owner.name.ToLower().Contains(filter.Value));
                    return results.ToList();

                case "address":
                    results = list.Where(owner => owner.address.ToLower().Contains(filter.Value));
                    return results.ToList(); ;
            }
            return null;   // Should never happen
        }



        public List<Owner> GetAllOwners()
        {
            return _ownerRepo.ReadAllOwners().ToList();
        }



        public Owner UpdateOwner(Owner ownerUpdate)
        {
            var updatedOwner = FindOwnerById(ownerUpdate.ownerId); //FindOwnerById(ownerUpdate.OwnerId);
            if (updatedOwner == null)
            {
                return null;
            }
            else
            {
                updatedOwner.name = ownerUpdate.name;
                updatedOwner.address = ownerUpdate.address;
                updatedOwner.petsOwned = ownerUpdate.petsOwned;
                return updatedOwner;
            }
        }



        public Owner DeleteOwner(int id)
        {
            return _ownerRepo.DeleteOwner(id);
        }

    }
}
