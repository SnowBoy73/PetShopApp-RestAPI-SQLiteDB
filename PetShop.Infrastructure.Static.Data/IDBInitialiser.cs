using System;
namespace PetShop.Infrastructure.Data
{
    public interface IDBInitialiser
    {
        void SeedDB(PetShopContext ctx);
    }
}
