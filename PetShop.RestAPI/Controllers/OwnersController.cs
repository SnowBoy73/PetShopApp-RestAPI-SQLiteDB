using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : Controller
    {

        private readonly IOwnerService _ownerService;
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;
        //public PetsController petsController;


        public OwnersController(IOwnerService ownerService, IPetService petService, IPetTypeService petTypeService)
        {
            _ownerService = ownerService;
            _petService = petService;
            _petTypeService = petTypeService;
        }



        // GET api/pets
        [HttpGet]
        public ActionResult<List<Owner>> Get([FromQuery] string prop, string val)//Filter filter) 
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
               
                if ((property == "name") || (property == "address"))
                {
                    List<Owner> searchedOwners = _ownerService.FindOwnersByProperty(filter);
                    if (searchedOwners.Count == 0)
                    {
                        return StatusCode(404, "No owner with the " + filter.Property + " '" + filter.Value + "' was  found");
                    }
                    else
                    {
                        return StatusCode(200, searchedOwners);
                    }
                }
                else
                {
                    return StatusCode(500, "Request Failed - The owner property '" + property + "' does not exist");
                }

            }
            else
            {
                return StatusCode(200, _ownerService.GetAllOwners());
            }
        }



        // GET api/owners/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Id must be greater than zero");
            }
            Owner ownerToGet = _ownerService.FindOwnerByIdIncludingPets(id);
            if (ownerToGet == null)
            {
                return StatusCode(404, "Request Failed - Unable to find this owner");
            }
            return StatusCode(200, ownerToGet);
        }



        // POST api/owners
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<Owner> Post([FromBody] Owner ownerToPost)
        {
            string error = CheckOwnerInput(ownerToPost);
            if (!(error == ""))
            {
                return StatusCode(500, error);
            }
            Owner ownerToCreate = _ownerService.CreateOwner(ownerToPost);
            return StatusCode(201, ownerToCreate);
        }



        // PUT api/owners/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner ownerToPut)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Owner id must be greater than zero");
            }
            if (id != ownerToPut.ownerId)
            {
                return StatusCode(500, "Request Failed - Owner id from header and owner id from JSON body do not match");
            }
            string error = CheckOwnerInput(ownerToPut);
            if (!(error == ""))
            {
                return StatusCode(500, error);
            }
            Owner ownerToUpdate = _ownerService.UpdateOwner(ownerToPut);
            if (ownerToUpdate == null)
            {
                return StatusCode(404, "No owner with id " + id + " was found to update ");
            }
            return StatusCode(202, ownerToUpdate);
        }



        // DELETE api/owners/5
        [HttpDelete("{id}")]
        public ActionResult<Owner> Delete(int id)
        {
            Owner deletedOwner;
            deletedOwner = _ownerService.DeleteOwner(id);
            if (deletedOwner == null)
            {
                return StatusCode(404, "No owner with id " + id + " was found to delete");
            }
            return StatusCode(202, "Owner with id " + id + " was deleted");
        }







        // Run check to ensure owner input is valid. Returns error message as a string
        private string CheckOwnerInput(Owner owner)
        {
            string error = "";  //used to determine error message (code 500)
            if (string.IsNullOrEmpty(owner.name))
            {
                error = "Request Failed - No owner name supplied";
            }

            if (string.IsNullOrEmpty(owner.address))
            {
                error = "Request Failed - No owner address supplied";
            }
            List<Pet> ownersPets = owner.petsOwned;
            if (ownersPets != null)
            {
                int count = 0;
                foreach (var pet in ownersPets)
                {
                    count++;
                    Pet petFromDB = _petService.FindPetById(pet.petId);
                    if (petFromDB == null)
                    {
                        error = "Request Failed - Pet id supplied does not exist for owned pet number " + count;
                    }
                    else
                    {
                        if ((pet.petId != petFromDB.petId) || (pet.name != petFromDB.name) || (pet.colour != petFromDB.colour) || (pet.birthDate != petFromDB.birthDate) || (pet.price != petFromDB.price) || (pet.soldDate != petFromDB.soldDate))
                        {
                            error = "Request Failed - Pet details supplied for owned pet number " + count + " is different from the details of the pet in the database with id " + pet.petId + ". Please correct the pets details or id to match a valid pet";
                        }
                        if (string.IsNullOrEmpty(pet.name))
                        {
                            error = "Request Failed - No pet name supplied for owned pet number " + count;
                        }

                        PetType petType = _petTypeService.FindPetTypeById(pet.type.petTypeId);
                        if (petType == null)
                        {
                            error = "Request Failed - Pet type Id supplied does not exist for owned pet number " + count;
                        }
                        else
                        {
                            if (pet.type.name != petType.name)
                            {
                                error = "Request Failed - Pet type name supplied for owned pet number " + count + " is different from the name of this pet type. Please correct the name or id to match a valid pet type";
                            }

                            if (pet.type.name == "")
                            {
                                error = "Request Failed - Pet type name not supplied for owned pet number " + count;
                            }
                        }
                        if (string.IsNullOrEmpty(pet.colour))
                        {
                            error = "Request Failed - No pet colour supplied for owned pet number " + count;
                        }

                        if (pet.birthDate < DateTime.Now.AddYears(-275))
                        {
                            error = "Request Failed - Birthdate is more than 275 years ago for owned pet number " + count + ". Henry the Tortoise is the oldest living animal at 275 years, so if this pet is older than that, you should contact the Guiness Book of Records";
                        }

                        if (pet.birthDate > DateTime.Now.AddDays(1))
                        {
                            error = "Request Failed - Birthdate for owned pet number " + count + " is in the future";
                        }

                        if (pet.price < 0)
                        {
                            error = "Request Failed - What? Are you going to pay someone to take owned pet number " + count + " away? Sounds like a terrible pet";
                        }

                        if (pet.soldDate < DateTime.Now.AddYears(-100))
                        {
                            error = "Request Failed - Sold date for owned pet number " + count + " is more than 100 years ago. If it was over a hundred years ago... who cares?";
                        }

                        if (pet.soldDate > DateTime.Now.AddDays(1))
                        {
                            error = "Request Failed - Sold date for owned pet number " + count + " is in the future";
                        }

                        if (pet.soldDate < pet.birthDate)
                        {
                            error = "Request Failed - Sold date for owned pet number " + count + " is before it's birthdate";
                        }

                        Owner previousOwner = _ownerService.FindOwnerById(pet.petOwner.ownerId);
                        if (pet.petOwner == null)
                        {
                            error = "Request Failed - Pet number " + count + " has no previous owner";
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

                    }
                }
            }
            return error;
        }


    }
}