using CreditCardChallenge.DataBase;
using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditCardChallenge.Repositories
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly CreditCardChallengeContext _context;

        public CreditCardRepository(CreditCardChallengeContext context)
        {
            _context = context;
        }

        public void Add(CreditCard newCreditCard)
        {
            _context.CreditCards.Add(newCreditCard);
            _context.SaveChanges();
        }

        public void Delete(string userId, int creditCardId)
        {
            var creditCard = Get(userId, creditCardId);
            _context.CreditCards.Remove(creditCard);
            _context.SaveChanges();
        }

        public CreditCard Get(string userId, int creditCardId)
        {            
            var result =  _context.CreditCards
                .Include(crc => crc.Purchases)
                .Where(crc => crc.Id == creditCardId && crc.UserId == userId)
                .SingleOrDefault();  
            
            if (result != null)                
                result.Purchases = result.Purchases.Where(pur => pur.BuyDate >= DateTime.UtcNow.AddDays(-30)).ToList();

            return result;
        }

        public List<CreditCard> GetAll(string UserId)
        {
            var query = _context.CreditCards.Where(t => t.UserId == UserId).AsQueryable();
            return query.ToList<CreditCard>();
        }
    }
}
