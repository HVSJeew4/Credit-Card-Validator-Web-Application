namespace CreditCardValidator.Models
{
    public class CardValidationRequest
    {
        public string CardNumber { get; set; }
    }

    public class CardValidationResponse
    {
        public bool IsValid { get; set; }
        public string CardType { get; set; }
        public string Message { get; set; }
    }
}