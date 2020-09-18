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
    public class SearchController : Controller
    {
        private readonly IPetService _petService;
        private readonly IPetTypeService _petTypeService;
        private readonly IOwnerService _ownerService;

        public SearchController(IPetService petService, IPetTypeService petTypeService, IOwnerService ownerService)
        {
            _petService = petService;
            _petTypeService = petTypeService;
            _ownerService = ownerService;
        }



        // GET api/search
        [HttpGet ("{ property}, {value}")]
        public ActionResult<List<Pet>> Get([FromQuery] string prop, string val)
        {
            //searchedPets = null;
            Filter filter = new Filter();
            string property = prop.ToLower();
            string value = val.ToLower();
            filter.Property = property;
            filter.Value = value;
            double priceCheck;
            if (!double.TryParse(val, out priceCheck))
            {
                return StatusCode(500, "Request Failed - Price given is not a number");
            }
            if ((property == "name") || (property == "name") || (property == "colour") || (property == "price") || (property == "previousowner"))
            //  if (property.Equals("name") ||)
            {
                List<Pet> searchedPets = _petService.FindPetsByProperty(filter);
                if (searchedPets == null)
                {
                    return StatusCode(404, "No pet with the " + property + " '" + val + "' was  found");
                }
                return StatusCode(200, searchedPets);
            }
            else
            {
                return StatusCode(500, "No pet with the " + property + " '" + value + "' was  found");
            }

        }

    }
}
