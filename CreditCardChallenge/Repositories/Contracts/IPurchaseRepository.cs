using CreditCardChallenge.Models;
using System;
using System.Collections.Generic;

namespace CreditCardChallenge.Repositories.Contracts
{
    public interface IPurchaseRepository
    {
        void Add(Purchase purchase);
        void Delete(int purchaseId, string userId);
        void Update(Purchase purchase);
        Purchase Get(int purchaseId, string userId, DateTime ? buyDate);
        public List<Purchase> GetAll(string userId, int creditCardId, DateTime? buyDate, int ? lastDays);
    }
}
