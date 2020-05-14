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

        public void Add(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }

        public void Update(Purchase datePurchase)
        {
            var purchase = Get(datePurchase.Id, datePurchase.UserId);
            if (purchase != null)
            {
                purchase.BuyDate = datePurchase.BuyDate;
                purchase.CreditCardId = datePurchase.CreditCardId;
                purchase.StoreName = datePurchase.StoreName;
                purchase.Value = datePurchase.Value;
                _context.Purchases.Update(purchase);
                _context.SaveChanges();
            }
        }

        public void Delete(int purchaseId, string userId)
        {
            var purchase = Get(purchaseId, userId);
            _context.Purchases.Remove(purchase);
            _context.SaveChanges();
        }

        public Purchase Get(int purchaseId, string userId, DateTime? buyDate = null)
        {
            var query = _context.Purchases.Include(pur => pur.CreditCard).Where(t => t.Id == purchaseId && t.UserId == userId).AsQueryable();

            if (buyDate != null)
            {
                query.Where(t => t.BuyDate == buyDate);
            }

            return query.FirstOrDefault();
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
    }
}
