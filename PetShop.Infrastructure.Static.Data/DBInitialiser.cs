using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using PetShop.Core.Entity;
using PetShop.Core.Helper;

namespace PetShop.Infrastructure.Data
{
    public class DBInitialiser: IDBInitialiser
    {
        private readonly IAuthenticationHelper _authenticationHelper;

        public DBInitialiser(IAuthenticationHelper authenticationHelper)
        {
            _authenticationHelper = authenticationHelper;
        }



        public void SeedDB(PetShopContext ctx)  // Using context. Could use repository but ctx is a cleaner change tracker
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            if (ctx.Pets.Any())
            {
                return;
            }

            List<Pet> pets = new List<Pet>
            {
                new Pet {IsComplete = true, name = "Works"},
                new Pet {IsComplete = false, name = " Not Works"},
            };
                
            string password = "1234";
            _authenticationHelper.CreatePasswordHash(password, out byte[] passwordHashAdmin,
                out byte[] passwordSaltAdmin);

            _authenticationHelper.CreatePasswordHash(password, out byte[] passwordHashUser,
                out byte[] passwordSaltUser); 
            
            //  Create Users
            List<User> users = new List<User>
            {
                new User
                    {
                        Username = "Admin",
                        PasswordHash = passwordHashAdmin,
                        PasswordSalt = passwordSaltAdmin,
                        IsAdmin = true
                    },

                    new User 
                    {
                        Username = "User",
                        PasswordHash = passwordHashUser,
                        PasswordSalt = passwordSaltUser,
                        IsAdmin = false
                    }
                };


//  Create PetTypes

            var petType1 = ctx.PetTypes.Add(new PetType()
            {
                name = "Snake"
            }).Entity;

            var petType2 = ctx.PetTypes.Add(new PetType()
            {
                name = "Giraffe"
            }).Entity;

            var petType3 = ctx.PetTypes.Add(new PetType()
            {
                name = "Jaguar"
            }).Entity;

            var petType4 = ctx.PetTypes.Add(new PetType()
            {
                name = "Wolverine"
            }).Entity;

            var petType5 = ctx.PetTypes.Add(new PetType()
            {
                name = "Bear"
            }).Entity;



//  Create Owners

            Owner owner1 = ctx.Owners.Add(new Owner()
            {
                name = "Cobber McCorker",
                address = "30 Happy Pet Lane",
                petsOwned = null
            }).Entity;

            Owner owner2 = ctx.Owners.Add(new Owner()
            {
                name = "Daevid Allen",
                address = "Planet Gong",
                petsOwned = null
            }).Entity;

            Owner owner3 = ctx.Owners.Add(new Owner()
            {
                name = "Billy Bully",
                address = "45 Snooze Pl",
                petsOwned = null
            }).Entity;

            Owner owner4 = ctx.Owners.Add(new Owner()
            {
                name = "Mike Muscles",
                address = "76 Strong St",
                petsOwned = null
            }).Entity;



//  Create Pets

            var pet1 = ctx.Pets.Add(new Pet()
            {
                name = "Geoff",
                type = petType1,
                birthDate = Convert.ToDateTime("1992-12-15"),
                soldDate = Convert.ToDateTime("2019-11-15"),
                colour = "Red",
                petOwner = owner2,
                price = 330
            }).Entity;

            Pet pet2 = ctx.Pets.Add(new Pet()
            {
                name = "Leo Long",
                type = petType2,
                birthDate = Convert.ToDateTime("1920-12-14"),
                soldDate = Convert.ToDateTime("2012-11-15"),
                colour = "Blue",
                petOwner = owner1,
                price = 1250
            }).Entity;

            Pet pet3 = ctx.Pets.Add(new Pet()
            {
                name = "Jimmy",
                type = petType3,
                birthDate = Convert.ToDateTime("1920-12-14"),
                soldDate = Convert.ToDateTime("2012-11-15"),
                colour = "Blue",
                petOwner = owner1,
                price = 340
            }).Entity;

            Pet pet4 = ctx.Pets.Add(new Pet()
            {
                name = "Bobby",
                type = petType4,
                birthDate = Convert.ToDateTime("1920-12-14"),
                soldDate = Convert.ToDateTime("2012-11-15"),
                colour = "Brown",
                petOwner = owner3,
                price = 888
            }).Entity;

            Pet pet5 = ctx.Pets.Add(new Pet()
            {
                name = "Sid",
                type = petType5,
                birthDate = Convert.ToDateTime("1920-12-14"),
                soldDate = Convert.ToDateTime("2012-11-15"),
                colour = "Orange",
                petOwner = owner3,
                price = 454
            }).Entity;

            Pet pet6 = ctx.Pets.Add(new Pet()
            {
                name = "Gus",
                type = petType3,
                birthDate = Convert.ToDateTime("1920-12-14"),
                soldDate = Convert.ToDateTime("2012-11-15"),
                colour = "Red",
                petOwner = owner2,
                price = 250
            }).Entity;

            ctx.Pets.AddRange(pets);
            ctx.Users.AddRange(users);
            ctx.SaveChanges();
        }
    }
}
