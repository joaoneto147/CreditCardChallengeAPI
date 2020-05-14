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

        [HttpPost]
        public ActionResult Add([FromBody]PurchaseDTO purchaseDTO)
        {
            var purchase = purchaseDTO.getPurchase(GetUserId());
            _purchaseRepository.Add(purchase);
            return Created("", purchase);
        }

        [HttpDelete("{purchaseId}")]
        public ActionResult Delete(int purchaseId)
        {
            _purchaseRepository.Delete(purchaseId, GetUserId());
            return Ok();
        }

        [HttpPut("{purchaseId}")]
        public ActionResult Update([FromBody] PurchaseDTO purchaseDTO, int purchaseId)
        {
            var purchase = purchaseDTO.getPurchase(GetUserId());
            purchase.Id = purchaseId;

            _purchaseRepository.Update(purchase);
            return Ok();
        }

        [Route("{purchaseId}")]
        [HttpGet]
        public ActionResult Get([FromQuery] DateTime buyDate, int purchaseId)
        {
            var result = _purchaseRepository.Get(purchaseId, GetUserId(), buyDate);
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

            var result = _purchaseRepository.GetAll(GetUserId(), creditCardId, buyDate, lastDays);

            if (result == null)
                return NotFound();
            else
                return Ok(new { result });
        }

        private string GetUserId()
        {
            return _userManager.GetUserId(User);
        }

    }
}