using CreditCardChallenge.DataBase;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly CreditCardChallengeContext _context;

        public PurchaseRepository(CreditCardChallengeContext context)
        {
            _context = context;
        }
        public void Add(int creditCardId, string storeName, double value)
        {
            var purchase = new Purchase
            {
                BuyDate = DateTime.UtcNow,
                StoreName = storeName,
                Value = value,
                CreditCardId = creditCardId
            };

            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }
        public Purchase Get(int id, DateTime? buyDate, string userId)
        {
            var query = _context.Purchases.Include(pur => pur.CreditCard).Where(t => t.Id == id).AsQueryable();

            if (buyDate != null)
            {
                query.Where(t => t.BuyDate == buyDate);
            }

            var result = query.FirstOrDefault();

            if (!ValidatePurchaseToUser(result.CreditCard, userId))
                result = null;

            return result;
        }
        public List<Purchase> GetAll(String userId, int creditCardId, DateTime? buyDate, int? lastDays)
        {
            if (!buyDate.HasValue)
            {
                lastDays = lastDays.HasValue ? lastDays : 1;
                buyDate = DateTime.UtcNow.AddDays(Math.Abs((double)lastDays) * -1);
            }

            var query = _context.Purchases.
                Where(
                    t => t.UserId == userId &&
                    t.CreditCardId == creditCardId &&
                    (!lastDays.HasValue ? t.BuyDate.Date == buyDate.Value.Date : t.BuyDate.Date >= buyDate.Value.Date)
                ).AsQueryable();



            return query.ToList<Purchase>();
        }

        private bool ValidatePurchaseToUser(CreditCard creditCard, string userId)
        {
            return creditCard.UserId.Equals(userId);
        }
    }
}
