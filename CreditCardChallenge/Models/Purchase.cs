using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreditCardChallenge.Models
{
    public class Purchase
    {
         public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [NotMapped]
        public ApplicationUser User { get; set; }
        public string StoreName { get; set; }
        public double Value { get; set; }
        public DateTime BuyDate { get; set; }
        [ForeignKey("CreditCard")]
        public int CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }

    }
}
