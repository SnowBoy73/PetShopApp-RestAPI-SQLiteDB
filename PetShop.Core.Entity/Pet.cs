using System;

namespace PetShop.Core.Entity
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public string Colour { get; set; }
        public DateTime BirthDate { get; set; }
        public double Price { get; set; }
        public DateTime SoldDate { get; set; }
        public Owner PreviousOwner { get; set; }
    }
}
