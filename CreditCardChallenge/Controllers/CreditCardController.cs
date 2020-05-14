using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories;
using CreditCardChallenge.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("Bearer")]
    public class CreditCardController : ControllerBase
    {
        private readonly ICreditCardRepository _creditCardRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreditCardController(ICreditCardRepository creditCardRepository, UserManager<ApplicationUser> userManager)
        {
            _creditCardRepository = creditCardRepository;
            _userManager = userManager;
        }

        [HttpPost]
        public ActionResult Add([FromBody]CreditCardDTO creditCardDTO)
        {
            var newCreditCard = new CreditCard
            {
                Number = creditCardDTO.Number,
                Holder = creditCardDTO.Holder,
                SecurityCode = creditCardDTO.SecurityCode,
                Flag = creditCardDTO.Flag,
                Limit = creditCardDTO.Limit,
                Purchases = creditCardDTO.Purchases,
                UserId = _userManager.GetUserAsync(HttpContext.User).Result.Id
            };

            _creditCardRepository.Add(newCreditCard);
            return Created("", newCreditCard);
        }

        [HttpDelete("{creditCardId}")]
        public ActionResult Delete(int creditCardId)
        {
            _creditCardRepository.Delete(
                _userManager.GetUserAsync(HttpContext.User).Result.Id, 
                creditCardId
            );
            return Ok();
        }

        [HttpPut("{creditCardId}")]
        public ActionResult Update([FromBody] CreditCardDTO creditCardDTO)
        {
            return Ok();
            //return Delete();
        }

        [HttpGet("{creditCardId}")]
        public ActionResult Get(int creditCardId)
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var creditCard = _creditCardRepository.Get(userId, creditCardId);

            if (creditCard != null)
                return Ok(creditCard);
            else
                return NoContent();
        }

        [HttpGet()]
        public ActionResult GetAll()
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var creditCards = _creditCardRepository.GetAll(userId);

            if (creditCards != null)
                return Ok(new { creditCards });
            else
                return NoContent();
        }
    }
}