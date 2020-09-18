using System;
using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService.Impl
{
    public class PetService : IPetService
    {
        readonly IPetRepository _petRepo;

        public PetService(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }


        public Pet NewPet(string name, PetType type, string colour, DateTime birthDate, double price, DateTime soldDate, Owner previousOwner)
        {
            var newPet = new Pet()
            {
                Name = name,
                Type = type,
                BirthDate = birthDate,
                SoldDate = soldDate,
                Colour = colour,
                PreviousOwner = previousOwner,
                Price = price
            };
            return newPet;
        }

        public Pet CreatePet(Pet createdPet)
        {
            return _petRepo.CreatePet(createdPet);
        }


        public Pet FindPetById(int id)
        {
            return _petRepo.ReadById(id);
        }


        public List<Pet> FindPetsByProperty(Filter filter)  //string prop, string searchValue)
        {
            IEnumerable<Pet> result;
            var list = _petRepo.ReadAllPets();
            switch (filter.Property)
            {
                case "name":
                    result = list.Where(pet => pet.Name.Contains(filter.Value));
                    return result.ToList(); ;

                case "colour":
                    result = list.Where(pet => pet.Colour.Contains(filter.Value));
                    return result.ToList();

                case "price":
                    double priceDouble = Convert.ToDouble(filter.Value);
                    result = list.Where(pet => pet.Price <= priceDouble);
                    return result.ToList();

                case "previouowner":
                    result = list.Where(pet => pet.PreviousOwner.Name.Contains(filter.Value));
                    return result.ToList();
            }
            return null;    // Should never happen
        }



        public List<Pet> GetAllPets()
        {
            return _petRepo.ReadAllPets().ToList();
        }



        public Pet UpdatePet(Pet petUpdate)
        {
            var updatedPet = FindPetById(petUpdate.PetId);
            if (updatedPet == null)
            {
                return null;
            }
            else
            {
                updatedPet.Name = petUpdate.Name;
                updatedPet.Type = petUpdate.Type;
                updatedPet.Colour = petUpdate.Colour;
                updatedPet.BirthDate = petUpdate.BirthDate;
                updatedPet.Price = petUpdate.Price;
                updatedPet.SoldDate = petUpdate.SoldDate;
                updatedPet.PreviousOwner = petUpdate.PreviousOwner;
                return updatedPet;
            }
        }



        public Pet DeletePet(int id)
        {
            return _petRepo.DeletePet(id);
        }

    }
}
