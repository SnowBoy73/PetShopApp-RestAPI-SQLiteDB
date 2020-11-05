using System;

namespace PetShop.Core.Entity
{
    public class Pet
    {
        public int petId { get; set; }
        public string name { get; set; }
        public PetType type { get; set; }
        public string colour { get; set; }
        public DateTime birthDate { get; set; }
        public double price { get; set; }
        public DateTime soldDate { get; set; }
        public Owner petOwner { get; set; }
        public bool IsComplete { get; set; }
    }
}
