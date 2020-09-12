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


        public PetsController(IPetService petService)
        {
            _petService = petService;
        }



        // GET: api/pets
        [HttpGet]
        public ActionResult<List<Pet>> Get(/*[FromQuery] string orderDir*/)
        //public ActionResult<IEnumerable<string>> Get(/*[FromQuery] string orderDir*/)
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
            Pet petToGet = _petService.FindPetById(id);
            if (petToGet == null)
            {
                return StatusCode(500, "Unable to find this pet");
            }
            return StatusCode(200, petToGet);
        }



        // POST api/pets
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<Pet> Post([FromBody] Pet petToPost) // Pet {pet} //string name, string type, string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner)
        {
            if (string.IsNullOrEmpty(petToPost.Name))
            {
                return BadRequest("No pet name supplied");
            }
            if (string.IsNullOrEmpty(petToPost.Type))
            {
                return BadRequest("No pet type supplied");
            }
            if (string.IsNullOrEmpty(petToPost.Colour))
            {
                return BadRequest("No pet colour supplied");
            }
            /*      DateTime birthDate;
                  if (DateTime.TryParse(petToPost.SoldDate, out birthDate))
                  {
                      return BadRequest("No pet birth date supplied");
                  }
                  double test;
                  if (!(Double.TryParse(petToPost.Price, out test)))
                  {
                      return BadRequest("No pet price supplied");
                  }
                  DateTime soldDate;
                  if (DateTime.TryParse(petToPost.SoldDate, out soldDate))
                  {
                      return BadRequest("No pet sold date supplied");
                  }
              */
            if (string.IsNullOrEmpty(petToPost.PreviousOwner))
            {
                return BadRequest("No pet previous owner supplied");
            }
            Pet petToCreate = _petService.CreatePet(petToPost);
            if (petToCreate == null)
            {
                return StatusCode(500, "Unable to create this pet");
            }
            return StatusCode(201, petToCreate);
        }



        // PUT api/pets/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet petToPut)  // string name, string type, string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner)
        {
            if (id < 1 || id != petToPut.PetId)
            {
                return BadRequest("Parameter PetId and petToPut.PetId do not match, or is less than 1");
            }
            Pet petToUpdate  = _petService.UpdatePet(petToPut);
            if (petToUpdate == null)
            {
                return StatusCode(404, "No pet with id " + id + " was found to delete ");
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
                return StatusCode(404, "No pet with id " + id + " was found to delete ");
            }
            return StatusCode(202, deletedPet);  // Ok($"Pet with id {id} was deleted");  //  deletedPet;
        }

    }
}
