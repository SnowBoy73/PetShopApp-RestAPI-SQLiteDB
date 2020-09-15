using System;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class FakeDB: IFakeDB
    {
        readonly IOwnerRepository _ownerRepo;
        readonly IPetTypeRepository _petTypeRepo;
        readonly IPetRepository _petRepo;


        public FakeDB(IOwnerRepository ownerRepository, IPetTypeRepository petTypeRepository, IPetRepository petRepository)
        {
            _ownerRepo = ownerRepository;
            _petTypeRepo = petTypeRepository;
            _petRepo = petRepository;
        }


        public void InitData()
        {


            // OWNERS
            Owner owner1 = new Owner()
            {
                Name = "Cobber McCorker",
                Address = "30 Happy Pet Lane",
                PetsOwned = null // new Pet() {PetId = 11})
            };
            _ownerRepo.CreateOwner(owner1);


            Owner owner2 = new Owner()
            {
                Name = "Daevid Allen",
                Address = "Planet Gong",
                PetsOwned = null
            };
            _ownerRepo.CreateOwner(owner2);


            Owner owner3 = new Owner()
            {
                Name = "Jake Roberts",
                Address = "12 Reptile Rd",
                PetsOwned = null
            };
            _ownerRepo.CreateOwner(owner3);


            Owner owner4 = new Owner()
            {
                Name = "Ginger Buns",
                Address = "43 Muffin Grove",
                PetsOwned = null
            };
            _ownerRepo.CreateOwner(owner4);


            Owner owner5 = new Owner()
            {
                Name = "Marty MyFries",
                Address = "1955 Future Path",
                PetsOwned = null
            };
            _ownerRepo.CreateOwner(owner5);


            Owner owner6 = new Owner()
            {
                Name = "Henry Hippo",
                Address = "90 Zoo Gardens",
                PetsOwned = null
            };
            _ownerRepo.CreateOwner(owner6);





            // PET TYPES
            PetType petType1 = new PetType()
            {
                Name = "Snake"
            };
            _petTypeRepo.CreatePetType(petType1);


            PetType petType2 = new PetType()
            {
                Name = "Cat"
            };
            _petTypeRepo.CreatePetType(petType2);


            PetType petType3 = new PetType()
            {
                Name = "Dog"
            };
            _petTypeRepo.CreatePetType(petType3);


            PetType petType4 = new PetType()
            {
                Name = "Wombat"
            };
            _petTypeRepo.CreatePetType(petType4);


            PetType petType5 = new PetType()
            {
                Name = "Fish"
            };
            _petTypeRepo.CreatePetType(petType5);


            PetType petType6 = new PetType()
            {
                Name = "Iguana"
            };
            _petTypeRepo.CreatePetType(petType6);


            PetType petType7 = new PetType()
            {
                Name = "Frog"
            };
            _petTypeRepo.CreatePetType(petType7);


            PetType petType8 = new PetType()
            {
                Name = "Rabbit"
            };
            _petTypeRepo.CreatePetType(petType8);


            PetType petType9 = new PetType()
            {
                Name = "Giraffe"
            };
            _petTypeRepo.CreatePetType(petType9);


            PetType petType10 = new PetType()
            {
                Name = "Spider"
                //SubType = "Black Widow"
            };
            _petTypeRepo.CreatePetType(petType10);
           // int test = _petTypeRepo.ReadAllPetTypes().ToList().Count;
           // Console.WriteLine($"Pet Type count = {test}");




            // PETS
            Pet pet1 = new Pet()
            {
                Name = "Geoff",
                Type = petType1,
                //       Gender = Convert.ToChar("F"),
                BirthDate = Convert.ToDateTime("1992-12-15"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Red",
                PreviousOwner = owner3,
                Price = 330
            };
            _petRepo.CreatePet(pet1);


            Pet pet2 = new Pet()
            {
                Name = "Leo Long",
                Type = petType6,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1920-12-14"),
                SoldDate = Convert.ToDateTime("2012-11-15"),
                Colour = "Blue",
                PreviousOwner = owner1,
                Price = 1250
            };
            _petRepo.CreatePet(pet2);


            Pet pet3 = new Pet()
            {
                Name = "Freddy",
                Type = petType7,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1930-12-14"),
                SoldDate = Convert.ToDateTime("2013-11-15"),
                Colour = "Orange",
                PreviousOwner = owner6,
                Price = 56
            };
            _petRepo.CreatePet(pet3);


            Pet pet4 = new Pet()
            {
                Name = "Jake",
                Type = petType1,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1940-12-14"),
                SoldDate = Convert.ToDateTime("2014-11-15"),
                Colour = "White",
                PreviousOwner = owner3,
                Price = 343
            };
            _petRepo.CreatePet(pet4);


            Pet pet5 = new Pet()
            {
                Name = "Jeremy",
                Type = petType2,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2010-12-14"),
                SoldDate = Convert.ToDateTime("2017-11-15"),
                Colour = "Blue",
                PreviousOwner = owner5,
                Price = 238
            };
            _petRepo.CreatePet(pet5);


            Pet pet6 = new Pet()
            {
                Name = "Albert",
                Type = petType3,
                //       Gender = Convert.ToChar("F"),
                BirthDate = Convert.ToDateTime("1992-12-15"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Black",
                PreviousOwner = owner2,
                Price = 330
            };
            _petRepo.CreatePet(pet6);


            Pet pet7 = new Pet()
            {
                Name = " Richard",
                Type = petType1,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2018-01-14"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Tan",
                PreviousOwner = owner5,
                Price = 165
            };
            _petRepo.CreatePet(pet7);


            Pet pet8 = new Pet()
            {
                Name = "Zues",
                Type = petType9,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2020-08-14"),
                SoldDate = Convert.ToDateTime("2020-08-15"),
                Colour = "Blue",
                PreviousOwner = owner2,
                Price = 50
            };
            _petRepo.CreatePet(pet8);


            Pet pet9 = new Pet()
            {
                Name = "Stan",
                Type = petType9,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2019-10-14"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "White",
                PreviousOwner = owner5,
                Price = 150
            };
            _petRepo.CreatePet(pet9);


            Pet pet10 = new Pet()
            {
                Name = "Selene",
                Type = petType5,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2017-12-14"),
                SoldDate = Convert.ToDateTime("2018-11-15"),
                Colour = "Blue and Yellow",
                PreviousOwner = owner1,
                Price = 970
            };
            _petRepo.CreatePet(pet10);

       
            Pet pet11 = new Pet()
            {
                Name = "Wally",
                Type = petType4,
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2016-06-04"),
                SoldDate = Convert.ToDateTime("2018-01-06"),
                Colour = "Brown",
                PreviousOwner = owner1,
                Price = 450
            };
            _petRepo.CreatePet(pet11);


        }
    }
}
