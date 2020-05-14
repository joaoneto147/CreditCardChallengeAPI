using System;
using System.Net;
using System.Net.Mail;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using CreditCardChallenge.Security;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
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

        [HttpPost("password/reset")]
        public ActionResult ResetPassword([FromQuery] string mail)
        {
            using (var smtp = new SmtpClient())
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(mail));
                message.From = new MailAddress("xxx@gmail.com");
                message.Subject = "Assunto";
                message.Body = "Seu Texto Aqui";
                message.IsBodyHtml = true;
                var credential = new NetworkCredential
                {
                    UserName = "xxxx@gmail.com",
                    Password = "xxxx"
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(message);
            }
            return Ok();
        }

        [HttpPut("/password/alter")]
        public ActionResult AlterPassword([FromQuery] string newPassword)
        {
            return Ok();
        }

    }
}
