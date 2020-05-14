using CreditCardChallenge.Models;
using System;

namespace CreditCardChallenge.Controllers
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public double Value { get; set; }
        public string BuyDate { get; set; }
        public int CreditCardId { get; set; }

        public Purchase getPurchase(string userId)
        {
            return new Purchase
            {
                BuyDate = DateTime.Parse(this.BuyDate),
                CreditCardId = this.CreditCardId,
                StoreName = this.StoreName,
                Value = this.Value,
                UserId = userId
            };
        }
    }
}