namespace CreditCardChallenge.Controllers
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string StoreName { get; set; }
        public double Value { get; set; }
        public string BuyDate { get; set; }
        public int CreditCardId { get; set; }
    }
}