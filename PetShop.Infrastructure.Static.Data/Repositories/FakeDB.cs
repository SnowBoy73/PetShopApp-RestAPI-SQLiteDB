using System;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data.Repositories
{
    public class FakeDB: IFakeDB
    {
        readonly IPetRepository _petRepo;


        public FakeDB(IPetRepository petRepository)
        {
            _petRepo = petRepository;
        }


        public void InitData()
        {

 /*           Pet pet1 = new Pet()
            {
                Name = "Geoff",
                Type = "Snake",
                //       Gender = Convert.ToChar("F"),
                BirthDate = Convert.ToDateTime("1992-12-15"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Red",
                PreviousOwner = "Jill Pill",
                Price = 330
            };
            _petRepo.CreatePet(pet1);


            Pet pet2 = new Pet()
            {
                Name = "Leo Long",
                Type = "Iguana",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1920-12-14"),
                SoldDate = Convert.ToDateTime("2012-11-15"),
                Colour = "Blue",
                PreviousOwner = "Cama Raad",
                Price = 1250
            };
            _petRepo.CreatePet(pet2);


            Pet pet3 = new Pet()
            {
                Name = "Freddy",
                Type = "Frog",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1930-12-14"),
                SoldDate = Convert.ToDateTime("2013-11-15"),
                Colour = "Orange",
                PreviousOwner = "Jimbo",
                Price = 56
            };
            _petRepo.CreatePet(pet3);


            Pet pet4 = new Pet()
            {
                Name = "Jake",
                Type = "Snake",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("1940-12-14"),
                SoldDate = Convert.ToDateTime("2014-11-15"),
                Colour = "White",
                PreviousOwner = "Sue Z",
                Price = 343
            };
            _petRepo.CreatePet(pet4);


            Pet pet5 = new Pet()
            {
                Name = "Jeremy",
                Type = "Cat",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2010-12-14"),
                SoldDate = Convert.ToDateTime("2017-11-15"),
                Colour = "Blue",
                PreviousOwner = "Bobby",
                Price = 238
            };
            _petRepo.CreatePet(pet5);


            Pet pet6 = new Pet()
            {
                Name = "Albert",
                Type = "Dog",
                //       Gender = Convert.ToChar("F"),
                BirthDate = Convert.ToDateTime("1992-12-15"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Black",
                PreviousOwner = "Barbara",
                Price = 330
            };
            _petRepo.CreatePet(pet6);


            Pet pet7 = new Pet()
            {
                Name = " Richard",
                Type = "Rabbit",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2018-01-14"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Tan",
                PreviousOwner = "Lilly Muffin",
                Price = 165
            };
            _petRepo.CreatePet(pet7);


            Pet pet8 = new Pet()
            {
                Name = "Zues",
                Type = "Mosquito",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2020-08-14"),
                SoldDate = Convert.ToDateTime("2020-08-15"),
                Colour = "Blue",
                PreviousOwner = "------",
                Price = 50
            };
            _petRepo.CreatePet(pet8);


            Pet pet9 = new Pet()
            {
                Name = "Stan",
                Type = "Stick Insect",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2019-10-14"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "White",
                PreviousOwner = "Bobby Springsteen",
                Price = 150
            };
            _petRepo.CreatePet(pet9);


            Pet pet10 = new Pet()
            {
                Name = "Selene",
                Type = "Tropical Fish",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2017-12-14"),
                SoldDate = Convert.ToDateTime("2018-11-15"),
                Colour = "Blue and Yellow",
                PreviousOwner = "Daevid Allen",
                Price = 970
            };
            _petRepo.CreatePet(pet10);

            Pet pet11 = new Pet()
            {
                Name = "Wally",
                Type = "Wombat",
                //      Gender = Convert.ToChar("M"),
                BirthDate = Convert.ToDateTime("2016-06-04"),
                SoldDate = Convert.ToDateTime("2018-01-06"),
                Colour = "Brown",
                PreviousOwner = "Cobber McCorker",
                Price = 450
            };
            _petRepo.CreatePet(pet11);
*/
        }
    }
}
