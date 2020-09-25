using System;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data
{
    public class DBInitialiser
    {
        public static void SeedDB(PetShopContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var petType1 = ctx.PetTypes.Add(new PetType()
            {
                Name = "Snake"
            }).Entity;

            var petType2 = ctx.PetTypes.Add(new PetType()
            {
                Name = "Giraffe"
            }).Entity;


            Owner owner1 = ctx.Owners.Add(new Owner()
            {
                Name = "Cobber McCorker",
                Address = "30 Happy Pet Lane",
                PetsOwned = null
            }).Entity;

            Owner owner2 = ctx.Owners.Add(new Owner()
            {
                Name = "Daevid Allen",
                Address = "Planet Gong",
                PetsOwned = null
            }).Entity;


            var pet1 = ctx.Pets.Add(new Pet()
            {
                //  PetId = 1,
                Name = "Geoff",
                Type = petType1,
                BirthDate = Convert.ToDateTime("1992-12-15"),
                SoldDate = Convert.ToDateTime("2019-11-15"),
                Colour = "Red",
                PreviousOwner = owner2,
                Price = 330
            }).Entity;

            Pet pet2 = new Pet()
            {
                Name = "Leo Long",
                Type = petType2,
                BirthDate = Convert.ToDateTime("1920-12-14"),
                SoldDate = Convert.ToDateTime("2012-11-15"),
                Colour = "Blue",
                PreviousOwner = owner1,
                Price = 1250
            };


        }
    }
}
