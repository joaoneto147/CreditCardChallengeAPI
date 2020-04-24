using CreditCardChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Repositories.Contracts
{
    public interface IUserRepository
    {
        public object CreateUser(ApplicationUser user, string password);                
    }
}
