using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardChallenge.Models
{
    public class CreditCardDTO
    {
        public int Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Holder { get; set; }
        [Required]
        public int SecurityCode { get; set; }
        [Required]
        public string Flag { get; set; }
        [Required]
        public double Limit { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
