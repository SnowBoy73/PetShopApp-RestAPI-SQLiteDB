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
    public class PetTypesController : Controller
    {

        private readonly IPetTypeService _petTypeService;


        public PetTypesController(IPetTypeService petTypeService)
        {
            _petTypeService = petTypeService;
        }



        // GET: api/petTypes
        //    [HttpGet]
        /*     public ActionResult<List<PetType>> Get()
             {
                 IEnumerable<PetType> allPetTypesENUM = _petTypeService.GetAllPetTypes();
                 List<PetType> allPetTypes = allPetTypesENUM.ToList();
                 if (allPetTypes.Count == 0)
                 {
                     return StatusCode(500, "There are no pet types in the pet types list");
                 }
                 return StatusCode(200, allPetTypes);
             }
        */


        // GET api/pets
        [HttpGet]
        public ActionResult<List<PetType>> Get([FromQuery] string prop, string val)//Filter filter) 
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

                if (property == "name")
                {
                    List<PetType> searchedPetTypes = _petTypeService.FindPetTypesByProperty(filter);
                    if (searchedPetTypes.Count == 0)
                    {
                        return StatusCode(404, "No pet type with the " + filter.Property + " '" + filter.Value + "' was  found");
                    }
                    else
                    {
                        return StatusCode(200, searchedPetTypes);
                    }
                }
                else
                {
                    return StatusCode(500, "Request Failed - The pet type property '" + property + "' does not exist");
                }

            }
            else
            {
                return StatusCode(200, _petTypeService.GetAllPetTypes());
            }
        }



        // GET api/petTypes/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Id must be greater than zero");
            }
            PetType petTypeToGet = _petTypeService.FindPetTypeById(id);
            if (petTypeToGet == null)
            {
                return StatusCode(404, "Request Failed - Unable to find this pet type");
            }
            return StatusCode(200, petTypeToGet);
        }



        // POST api/petTypes
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<PetType> Post([FromBody] PetType petTypeToPost)
        {
            if (string.IsNullOrEmpty(petTypeToPost.Name))
            {
                return StatusCode(500, "No name of pet type supplied");
            }
            List<PetType> allPetTypes = _petTypeService.GetAllPetTypes();
            foreach (var petType in allPetTypes)
            {
                if (petType.Name == petTypeToPost.Name)
                {
                    return StatusCode(500, "This pet type with name " + petType.Name + " already exists with an id of " + petType.PetTypeId);
                }
            }
            PetType petTypeToCreate = _petTypeService.CreatePetType(petTypeToPost);
            return StatusCode(201, petTypeToCreate);
        }



        // PUT api/petTypes/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType petTypeToPut)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - Pet type id is less than 1");
            }

            if (id != petTypeToPut.PetTypeId)
            {
                return StatusCode(500, "Request Failed - Pet type id from header and Pet type id from JSON body do not match");
            }
            if (string.IsNullOrEmpty(petTypeToPut.Name))
            {
                return StatusCode(500, "No name of pet type supplied");
            }
            PetType petTypeToUpdate = _petTypeService.UpdatePetType(petTypeToPut);
            if (petTypeToUpdate == null)
            {
                return StatusCode(404, "No pet type with id " + id + " was found to update ");
            }
            return StatusCode(202, petTypeToUpdate);
        }



        // DELETE api/petTypes/5
        [HttpDelete("{id}")]
        public ActionResult<PetType> Delete(int id)
        {
            PetType deletedPetType;
            deletedPetType = _petTypeService.DeletePetType(id);
            if (deletedPetType == null)
            {
                return StatusCode(404, "No owner with id " + id + " was found to delete ");
            }
            return StatusCode(202, deletedPetType);
        }


    }
}
