using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetShop.Core.Entity;
using PetShop.Core.DomainService;
using PetShop.Core.Helper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PetShop.RestAPI.Controllers
{
    [Route("/token")]
    public class Token : Controller
    {
     
            private IUserRepository repository;
            private IAuthenticationHelper authenticationHelper;

            public Token(IUserRepository repos, IAuthenticationHelper authHelper)
            {
                repository = repos;
                authenticationHelper = authHelper;
            }

            [HttpPost]
            public IActionResult Login([FromBody] LoginInputModel model)
            {
                var user = repository.ReadAllUsers().FirstOrDefault(u => u.Username == model.Username);

                // check if username exists
                if (user == null)
                    return Unauthorized();

                // check if password is correct
                if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                    return Unauthorized();

                // Authentication successful
                return Ok(new
                {
                    username = user.Username,
                    token = authenticationHelper.GenerateToken(user)
                });
            }

    }

}
