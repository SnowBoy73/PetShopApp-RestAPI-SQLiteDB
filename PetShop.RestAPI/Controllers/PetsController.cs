using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")] // api/
    [ApiController]
    public class PetsController : Controller//Base
    {
      
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;
        private readonly IOwnerService _ownerService;

        public PetsController(IPetService petService, IPetTypeService petTypeService, IOwnerService ownerService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
            _ownerService = ownerService;
        }



        // GET api/pets
        /*   [HttpGet]
           public ActionResult<List<Pet>> Get([FromQuery] string prop, string val)//Filter filter)
           {
               if (prop != null)
               {
                   if (val == null)
                   {
                       return StatusCode(500, "Request Failed - no value provided for search");
                   }
                   Filter filter = new Filter();
                   string property = prop.ToLower();
                   string value = val.ToLower();
                   filter.Property = property;
                   filter.Value = value;
                   if (property == "price")
                   {
                       double priceCheck;
                       if (!double.TryParse(filter.Value, out priceCheck))
                       {
                           return StatusCode(500, "Request Failed - Price given is not a number");
                       }
                   }
                   if ((property == "name") || (property == "colour") || (property == "price") || (property == "previousowner"))
                   {
                       List<Pet> searchedPets = _petService.FindPetsByProperty(filter);
                       if (searchedPets.Count == 0)
                       {
                           return StatusCode(404, "No pet with the " + filter.Property + " '" + filter.Value + "' was  found");
                       }
                       else
                       {
                           return StatusCode(200, searchedPets);
                       }
                   }
                   else
                   {
                       return StatusCode(500, "Request Failed - The pet property '" + property + "' does not exist");
                   }

               }
               else
               {
                   return StatusCode(200, _petService.GetAllPets());
               }   
           } */



        // GET api/pets
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get([FromQuery] PagingFilter filter)
        {
            var petz = _petService.GetAllPets();
            return Ok(petz);
        }



        // GET api/pets/5
        [Authorize(Roles = "Administrator")]
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
                return StatusCode(404, "No pet with id " + id + " was found");
            }
            return StatusCode(200, petToGet);
        }



        // POST api/pets
        [Authorize(Roles = "Administrator")]
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<Pet> Post([FromBody] Pet petToPost)
        {
            /*   string error = CheckPetInput(petToPost);
               if (!(error == ""))
               {
                   return StatusCode(500, error);
               }
               Pet petToCreate = _petService.CreatePet(petToPost);
               return StatusCode(201, petToCreate); 
               */
            try
            {
                return Ok(_petService.CreatePet(petToPost));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // PUT api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Pet> Put(int id, [FromBody] Pet petToPut)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Pet id must be greater than zero");
            }
            if (id != petToPut.petId)
            {
                return StatusCode(500, "Request Failed - Pet id from header and Pet id from JSON body do not match");
            }
            string error = CheckPetInput(petToPut);
            if (!(error == ""))
            {
                return StatusCode(500, error);
            }
            Pet petToUpdate  = _petService.UpdatePet(petToPut);
            if (petToUpdate == null)
            {
                return StatusCode(404, "No pet with id " + id + " was found to update");
            }
            return StatusCode(202, petToUpdate);
}



// DELETE api/pets/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            Pet deletedPet;
            deletedPet = _petService.DeletePet(id);
            if (deletedPet == null)
            {
                return StatusCode(404, "No pet with id " + id + " was found to delete");
            }
            return StatusCode(202, "Pet with id " + id + " was deleted");
        }


        // Run check to ensure Pet input is valid. Returns error message as a string
        private string CheckPetInput(Pet pet)
        {
            string error = "";  //used to determine error message (code 500)
            if (string.IsNullOrEmpty(pet.name))
            {
                error = "Request Failed - No pet name supplied";
            }

            PetType petType = _petTypeService.FindPetTypeById(pet.type.petTypeId);
            if (petType == null)
            {
                error = "Request Failed - Pet type Id supplied does not exist";
            }
            else
            {
                if (pet.type.name != petType.name)
                {
                    error = "Request Failed - Pet type name supplied is different from the name of this pet type. Please correct the name or id to match a valid pet type";
                }

                if (pet.type.name == "")
                {
                    error = "Request Failed - Pet type name not supplied";
                }
            }
            if (string.IsNullOrEmpty(pet.colour))
            {
                error = "Request Failed - No pet colour supplied";
            }

            if (pet.birthDate < DateTime.Now.AddYears(-275))
            {
                error = "Request Failed - Birthdate is more than 275 years ago. Henry the Tortoise is the oldest living animal at 275 years, so if this pet is older than that, you should contact the Guiness Book of Records";
            }

            if (pet.birthDate > DateTime.Now.AddDays(1))
            {
                error = "Request Failed - Birthdate is in the future";
            }

            if (pet.price < 0)
            {
                error = "Request Failed - What? Are you going to pay someone to take the pet away? Sounds like a terrible pet";
            }

            if (pet.soldDate < DateTime.Now.AddYears(-100))
            {
                error = "Request Failed - Sold date is more than 100 years ago. If it was over a hundred years ago... who cares?";
            }

            if (pet.soldDate > DateTime.Now.AddDays(1))
            {
                error = "Request Failed - Sold date is in the future";
            }

            if (pet.soldDate < pet.birthDate)
            {
                error = "Request Failed - Sold date is before birthdate";
            }

            Owner previousOwner = _ownerService.FindOwnerById(pet.petOwner.ownerId);
            if (previousOwner == null)
            {
                error = "Request Failed - Previous owner Id supplied does not exist";
            }
            else
            {
                if (pet.petOwner.name != previousOwner.name)
                {
                    error = "Request Failed - Name of previous owner supplied does not match the owner with id " + previousOwner.ownerId;
                }

                if (pet.petOwner.address != previousOwner.address)
                {
                    error = "Request Failed - Address of previous owner supplied does not match the owner with id " + previousOwner.ownerId;
                }
            }
            return error;
        }
    }
}
