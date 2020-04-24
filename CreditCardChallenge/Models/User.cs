using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardChallenge.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
    }
}
