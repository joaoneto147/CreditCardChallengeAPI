using CreditCardChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Repositories.Contracts
{
    public interface ICreditCardRepository
    {
        void Add(CreditCard dadosCreditCard);
        void Delete(string userId, int creditCardId);
        public CreditCard Get(string userId, int creditCardId);
        List<CreditCard> GetAll(string UserId);
    }
}
