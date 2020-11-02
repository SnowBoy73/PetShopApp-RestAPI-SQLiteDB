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
    public class UsersController : ControllerBase
    {

        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;
        private readonly IOwnerService _ownerService;
        private readonly IUserService _userService;


        public UsersController(IPetService petService, IPetTypeService petTypeService, IOwnerService ownerService, IUserService userService)  // maybe not
        {
            _petService = petService;
            _petTypeService = petTypeService;
            _ownerService = ownerService;
            _userService = userService;

        }



        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get([FromQuery] PagingFilter filter)
        {
            var userz = _petService.GetAllPets();
            return Ok(userz);
        }



        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            if (id < 1)
            {
                return BadRequest("Request Failed - Id must be greater than zero");
            }
            User userToGet = _userService.FindUserById(id);
            if (userToGet == null)
            {
                return StatusCode(404, "No user with id " + id + " was found");
            }
            return StatusCode(200, userToGet);
        }



        // POST api/pets
        [HttpPost]  // NOT essential. Only needed if we change this methods name from "Post", and then it tells the system this is the POST method. Needed if sending parameters
        public ActionResult<User> Post([FromBody] User userToPost)
        {
            try
            {
                return Ok(_userService.CreateUser(userToPost));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // PUT api/users/5
        [HttpPut("{id}")]
        public ActionResult<User> Put(int id, [FromBody] User userToPut)
        {
            if (id < 1)
            {
                return StatusCode(500, "Request Failed - User id must be greater than zero");
            }
            if (id != userToPut.UserId)
            {
                return StatusCode(500, "Request Failed - User id from header and user id from JSON body do not match");
            }
           
            User userToUpdate = _userService.UpdateUser(userToPut);
            if (userToUpdate == null)
            {
                return StatusCode(404, "No user with id " + id + " was found to update");
            }
            return StatusCode(202, userToUpdate);
        }



        // DELETE api/pets/5
        [HttpDelete("{id}")]
        public ActionResult<User> Delete(int id)
        {
            User deletedUser;
            deletedUser = _userService.DeleteUser(id);
            if (deletedUser == null)
            {
                return StatusCode(404, "No User with id " + id + " was found to delete");
            }
            return StatusCode(202, "User with id " + id + " was deleted");
        }


    }
}

