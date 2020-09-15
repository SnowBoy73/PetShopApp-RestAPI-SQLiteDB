using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
      
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;

        public PetsController(IPetService petService, IPetTypeService petTypeService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
        }



        // GET: api/pets
        [HttpGet]
        public ActionResult<List<Pet>> Get(/*[FromQuery] string orderDir*/)
        {
            IEnumerable<Pet> allPetsENUM = _petService.GetAllPets();
            List<Pet> allPets = allPetsENUM.ToList();
            if (allPets.Count == 0)
            {
                return StatusCode(500, "There are no pets in the pet shop");
            }
            return StatusCode(200, allPets);
        }



        // GET api/pets/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Request Failed - Id must be greater than zero");
            }
            Pet petToGet = _petService.FindPetById(id);
            if (petToGet == null)
            {
                return StatusCode(404, "No pet with id " + id + " was not found");
            }
            return StatusCode(200, petToGet);
        }



        // POST api/pets
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<Pet> Post([FromBody] Pet petToPost) // Pet {pet} //string name, string type, string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner)
        {
            string error = CheckPetInput(petToPost);
            if (!(error == ""))
            {
                return StatusCode(500, error);
            }
            Pet petToCreate = _petService.CreatePet(petToPost);
            if (petToCreate == null)
            {
                return StatusCode(404, "Unable to create this pet");
            }
            return StatusCode(201, petToCreate);
        }



        // PUT api/pets/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet petToPut)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Id must be greater than zero");
            }
            if (id != petToPut.PetId)
            {
                return StatusCode(500, "Parameter PetId and petToPut.PetId do not match");
            }
            string error = CheckPetInput(petToPut);
            if (!(error == ""))
            {
                return StatusCode(500, error);
            }
           // CheckPetInput(petToPut);
            Pet petToUpdate  = _petService.UpdatePet(petToPut);
            if (petToUpdate == null)
            {
                return StatusCode(404, "No pet with id " + id + " was not found to update");
            }
            return StatusCode(202, petToUpdate);
        }



        // DELETE api/pets/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            Pet deletedPet;
            deletedPet = _petService.DeletePet(id);
            if (deletedPet == null)
            {
                return StatusCode(404, "No pet with id " + id + " was not found to delete");
            }
            return StatusCode(202, deletedPet);  // Ok($"Pet with id {id} was deleted");  //  deletedPet;
        }



        private string CheckPetInput(Pet pet)
        {
            string error = "";
            if (string.IsNullOrEmpty(pet.Name))
            {
                error = "Request Failed - No pet name supplied";
            }

           
            PetType petType = _petTypeService.FindPetTypeById(pet.Type.PetTypeId);
            if (petType == null)
            {
                error = "Request Failed - Pet type Id supplied does not exist";
            }

            if (pet.Type.Name != petType.Name)
            {
                 error = "Request Failed - Pet type name supplied id different from the name of this pet type. Please correct the name or id to match a valid pet type";
            }
            
            if (string.IsNullOrEmpty(pet.Colour))
            {
                error = "No pet colour supplied";
            }

            if (pet.BirthDate < DateTime.Now.AddYears(-275))
            {
                error = "Request Failed - Birthdate is more than 275 years ago. Henry the Tortoise is the oldest living animal at 275 years, so if this pet is older than that, you should contact the Guiness Book of Records";
            }

            if (pet.BirthDate > DateTime.Now.AddDays(1))
            {
                error = "Request Failed - Birthdate is in the future";
            }

            if (pet.Price < 0)
            {
                error = "Request Failed - What? Are you going to pay someone to take the pet away? Sounds like a terrible pet";
            }
           
            if (pet.SoldDate < DateTime.Now.AddYears(-100))
            {
                error = "Request Failed - Sold date is more than 100 years ago. If it was over a hundred years ago... who cares?";
            }

            if (pet.SoldDate > DateTime.Now.AddDays(1))
            {
                error = "Request Failed - Sold date is in the future";
            }

            if (pet.SoldDate < pet.BirthDate)
            {
                error = "Request Failed - Sold date is before birthdate";
            }

            return error;
        }
    }
}
