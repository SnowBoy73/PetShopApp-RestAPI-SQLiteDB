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


        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }



        // GET: api/owners
        [HttpGet]
        public ActionResult<List<Owner>> Get(/*[FromQuery] string orderDir*/)
        //public ActionResult<IEnumerable<string>> Get(/*[FromQuery] string orderDir*/)
        {
            IEnumerable<Owner> allOwnersENUM = _ownerService.GetAllOwners();
            List<Owner> allOwners = allOwnersENUM.ToList();

            if (allOwners.Count == 0)
            {
                return StatusCode(500, "There are no owners in the owner list");
            }
            return StatusCode(200, allOwners);
        }



        // GET api/owners/5
        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Request Failed - Id must be greater than zero");
            }
            Owner ownerToGet = _ownerService.FindOwnerByIdIncludingPets(id); //FindOwnerById(id);

            Console.WriteLine($"name {ownerToGet.Name}  address {ownerToGet.Address}  pets# {ownerToGet.PetsOwned.Count()}");
            if (ownerToGet == null)
            {
                return StatusCode(404, "Unable to find this owner");
            }
            return ownerToGet; // StatusCode(200, ownerToGet);
        }



        // POST api/owners
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<Owner> Post([FromBody] Owner ownerToPost) // Pet {pet} //string name, string type, string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner)
        {
            if (string.IsNullOrEmpty(ownerToPost.Name))
            {
                return BadRequest("No name of owner supplied");
            }
            if (string.IsNullOrEmpty(ownerToPost.Address))
            {
                return BadRequest("No address of owner supplied");
            }
         /*   if (List<Pet> == null) .IsNullOrEmpty(ownerToPost.PetsOwned))
            {
                return BadRequest("No pet colour supplied");
            }
         */ 
            Owner ownerToCreate = _ownerService.CreateOwner(ownerToPost);
            if (ownerToCreate == null)
            {
                return StatusCode(500, "Unable to create this owner");
            }
            return StatusCode(201, ownerToCreate);
        }



        // PUT api/owners/5
        [HttpPut("{id}")]
        public ActionResult<Owner> Put(int id, [FromBody] Owner ownerToPut)  // string name, string type, string colour, DateTime birthDate, double price, DateTime soldDate, string previousOwner)
        {
            if (id < 1 || id != ownerToPut.OwnerId)
            {
                return BadRequest("Parameter OwnerId and ownerToPut.OwnerId do not match, or is less than 1");
            }
            Owner ownerToUpdate = _ownerService.UpdateOwner(ownerToPut);
            if (ownerToUpdate == null)
            {
                return StatusCode(404, "No owner with id " + id + " was found to delete ");
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
                return StatusCode(404, "No owner with id " + id + " was found to delete ");
            }
            return StatusCode(202, deletedOwner);  // Ok($"Owner with id {id} was deleted"); 
        }

    }
}
