using CreditCardChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Repositories.Contracts
{
    public interface IPurchaseRepository
    {
        void Add(int creditCardId, string storeName, double value);
        Purchase Get(int id, DateTime ? buyDate, string userId);
        public List<Purchase> GetAll(String userId, int creditCardId, DateTime? buyDate, int ? lastDays);
    }
}
