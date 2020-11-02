    using System;
using System.Collections.Generic;

namespace PetShop.Core.Entity
{
    public class Owner
    {
        public int ownerId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public List<Pet> petsOwned { get; set; }
    }
}
