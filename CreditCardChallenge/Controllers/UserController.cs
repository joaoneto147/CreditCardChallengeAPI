using System;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using CreditCardChallenge.Security;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public object Login([FromBody] User user, [FromServices]AccessManager accessManager)
        {
            var userAutentication = accessManager.ValidateCredentials(user);
            if (userAutentication.Authenticated)
            {
                return accessManager.GenerateToken(userAutentication);
            }
            else
            {
                return new
                {
                    Authenticated = false,
                    Message = "Authentication failed!"
                };
            }
        }

        [HttpPost("")]
        public ActionResult CreateUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid)                
                return UnprocessableEntity(userDTO);
           
            var newUser = new ApplicationUser
            {
                FullName = userDTO.Name,
                Email = userDTO.Email,
                UserName = userDTO.Email
            };

            dynamic userCreated = _userRepository.CreateUser(newUser, userDTO.Password);
            return userCreated.created ? Created("", userCreated) : Ok(userCreated);
        }        
    }
}