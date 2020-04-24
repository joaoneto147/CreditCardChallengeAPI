using System;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CreditCardChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchaseController(IPurchaseRepository purchaseRepository, UserManager<ApplicationUser> userManager)
        {
            _purchaseRepository = purchaseRepository;
            _userManager = userManager;
        }

        [Route("{purchaseId}")]
        [HttpGet]
        public ActionResult Get([FromQuery] DateTime buyDate, int purchaseId)
        {
            var userId = _userManager.GetUserAsync(HttpContext.User).Result.Id;
            var result = _purchaseRepository.Get(purchaseId, buyDate, userId);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult GetAll([FromQuery] DateTime? buyDate, int creditCardId, [FromQuery] int? lastDays)
        {
            if (!buyDate.HasValue && !lastDays.HasValue)
                return BadRequest("Date filter not informed!");

            var userId = _userManager.GetUserId(User);

            var result = _purchaseRepository.GetAll(userId, creditCardId, buyDate, lastDays);

            if (result == null)
                return NotFound();
            else
                return Ok(new {result});
        }

    }
}