using System;
using System.Collections.Generic;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class PetRepository : IPetRepository
    {
        static int id = 1; // as DB cannot have an id of 0
        private static List<Pet> _pets = new List<Pet>();



     /*   public PetRepository()   // NEW constr.
        {
        }

*/
        public Pet CreatePet(Pet pet)
        {
            pet.PetId = id++;
            _pets.Add(pet);
            return pet;
        }



        public IEnumerable<Pet> ReadAllPets()
        {
            return _pets;
        }


        public Pet ReadById(int id)
        {
            foreach (var pet in _pets)
            {
                if (pet.PetId == id)
                {
                    return pet;
                }
            }
            return null;
        }



        // Remove later for UOW
        public Pet UpdatePet(Pet petUpdate)
        {
            var petFromDB = this.ReadById(petUpdate.PetId);
            if (petFromDB != null)
            {
                petFromDB.Name = petUpdate.Name;
                petFromDB.Type = petUpdate.Type;
                petFromDB.BirthDate = petUpdate.BirthDate;
                petFromDB.SoldDate = petUpdate.SoldDate;
                petFromDB.Colour = petUpdate.Colour;
                petFromDB.PreviousOwner = petUpdate.PreviousOwner;
                petFromDB.Price = petUpdate.Price;
                return petFromDB;
            }
            return null;
        }


        public Pet DeletePet(int id)
        {
            var petFound = this.ReadById(id);
            if (petFound != null)
            {
                _pets.Remove(petFound);
                return petFound;
            }
            return null;
        }

    }

}
