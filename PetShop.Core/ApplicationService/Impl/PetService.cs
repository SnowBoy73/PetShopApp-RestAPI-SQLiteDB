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
                //         Gender = gender,
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


        public List<Pet> FindPetsByProperty(string prop, string searchValue)
        {
            IEnumerable<Pet> query;
            var list = _petRepo.ReadAllPets();
            switch (prop)
            {
                case "1":
                    query = list.Where(pet => pet.Name.ToLower().Contains(searchValue.ToLower()));
                    return query.ToList(); ;

                case "2":
                    //   query = list.Where(pet => pet.Type.ToLower().Contains(searchValue.ToLower()));
                    return null; //query.ToList(); ;

                case "3":
                    if (searchValue == "Y" || searchValue == "y")
                        query = list.OrderBy(pet => pet.Price);
                    else
                        query = list.OrderBy(pet => pet.Price).Reverse();
                    return query.ToList();

                default:
                    Console.WriteLine("That is not a valid property");  // Shouldn't happen, alresdy exceptioned
                   break;
            }
            return null;
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
                //     updatedPet.Gender = gender;
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
