using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Models
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Holder { get; set; }
        public int SecurityCode { get; set; }
        public string Flag { get; set; }
        public double Limit { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
