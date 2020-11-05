using System;
using System.Collections.Generic;
using System.IO;
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
                name = name,
                type = type,
                birthDate = birthDate,
                soldDate = soldDate,
                colour = colour,
                petOwner = previousOwner,
                price = price
            };
            return newPet;
        }



        public Pet CreatePet(Pet createdPet)
        {
            if (createdPet.name == null || createdPet.name == "")
                throw new InvalidDataException("To create a pet you need to name the pet");
            if (createdPet.type == null )
                throw new InvalidDataException("To create a pet you need a pet type");
            if (createdPet.colour == null || createdPet.colour == "")
                throw new InvalidDataException("To create a pet you need to nagive it a colour");
            if (createdPet.birthDate == null)
                throw new InvalidDataException("To create a pet you need to declare it's birth date");
            if (createdPet.price <= 0)
                throw new InvalidDataException("To create a pet you need to give it a valid price");
            if (createdPet.soldDate == null)
                throw new InvalidDataException("To create a pet you need to declare it's sold date");
            if (createdPet.petOwner == null || createdPet.petOwner.ownerId <= 0)
                throw new InvalidDataException("To create a pet you need a previous owner");
            return _petRepo.CreatePet(createdPet);
        }



        public Pet FindPetById(int id)
        {
            return _petRepo.ReadById(id);
        }


       
        public List<Pet> FindPetsByProperty(Filter filter)
        {
            IEnumerable<Pet> result;
            var list = _petRepo.ReadAllPets();
            switch (filter.Property)
            {
                case "name":
                    result = list.Where(pet => pet.name.ToLower().Contains(filter.Value));
                    return result.ToList();

                case "colour":
                    result = list.Where(pet => pet.colour.ToLower().Contains(filter.Value));
                    return result.ToList();

                case "price":
                    double priceDouble = Convert.ToDouble(filter.Value);
                    result = list.Where(pet => pet.price <= priceDouble);
                    return result.ToList();

                case "previousowner":
                    result = list.Where(pet => pet.petOwner.name.ToLower().Contains(filter.Value));
                    return result.ToList();
            }
            return null;    // Should never happen
        }



        public IEnumerable<Pet> GetAllPets()
        {
            return _petRepo.ReadAllPets();
        }



        public Pet UpdatePet(Pet petUpdate)
        {
            return _petRepo.UpdatePet(petUpdate);
        }



        public Pet DeletePet(int id)
        {
            return _petRepo.DeletePet(id);
        }

    }
}
