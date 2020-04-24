using CreditCardChallenge.Models;
using CreditCardChallenge.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public object CreateUser(ApplicationUser user, string password)
        {
            string returnMessage;
            var createReturn = _userManager.CreateAsync(user, password).Result;
           
            if (createReturn.Succeeded)
                returnMessage = "Account has been successfully created!";
            else
            {
                List<string> erros = new List<string>();
                foreach (var erro in createReturn.Errors)
                {
                    erros.Add(erro.Description);
                }

                returnMessage = ($"Unable to create account!{string.Join("\n", erros)}");
            }
            return new
            {
                user = user.Email,
                created = createReturn.Succeeded,
                message = returnMessage
            };
        }
    }
}
