namespace CreditCardValidator.Models
{
    public class CardValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public string CardType { get; set; }
    }
}
