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
        [HttpGet]
        public ActionResult<List<PetType>> Get(/*[FromQuery] string orderDir*/)
        {
            IEnumerable<PetType> allPetTypesENUM = _petTypeService.GetAllPetTypes();
            List<PetType> allPetTypes = allPetTypesENUM.ToList();
            if (allPetTypes.Count == 0)
            {
                return StatusCode(500, "There are no pet types in the pet types list");
            }
            return StatusCode(200, allPetTypes);
        }



        // GET api/petTypes/5
        [HttpGet("{id}")]
        public ActionResult<PetType> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Request Failed - Id must be greater than zero");
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
                return BadRequest("No name of pet type supplied");
            }
            PetType petTypeToCreate = _petTypeService.CreatePetType(petTypeToPost);
            if (petTypeToCreate == null)
            {
                return StatusCode(500, "Unable to create this pet type");
            }
            return StatusCode(201, petTypeToCreate);
        }



        // PUT api/petTypes/5
        [HttpPut("{id}")]
        public ActionResult<PetType> Put(int id, [FromBody] PetType petTypeToPut)
        {
            if (id < 1 || id != petTypeToPut.PetTypeId)
            {
                return BadRequest("Parameter PetTypeId and PetTypeToPut.OwnerId do not match, or is less than 1");
            }
            PetType petTypeToUpdate = _petTypeService.UpdatePetType(petTypeToPut);
            if (petTypeToUpdate == null)
            {
                return StatusCode(404, "No pet type with id " + id + " was found to delete ");
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
            return StatusCode(202, deletedPetType);  // Ok($"Pet Type with id {id} was deleted"); 
        }

    }
}
