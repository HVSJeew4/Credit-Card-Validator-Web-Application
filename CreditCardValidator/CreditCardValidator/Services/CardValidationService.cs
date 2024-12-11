using CreditCardValidator.Models;
using CreditCardValidator.Services;
using CreditCardValidatorAPI.Services;

namespace CreditCardValidator.Services
{
    public class CardValidationService : ICardValidationService
    {
        public CardValidationService()
        {
        }

        public CardValidationResult ValidateCard(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || !cardNumber.All(char.IsDigit))
            {
                return new CardValidationResult { IsValid = false, Message = "Invalid card number format." };
            }

            if (!IsValidLuhn(cardNumber))
            {
                return new CardValidationResult { IsValid = false, Message = "Card number failed Luhn check." };
            }

            var cardType = GetCardType(cardNumber);
            if (cardType == "Unknown")
            {
                return new CardValidationResult { IsValid = false, Message = "Unsupported card type." };
            }

            return new CardValidationResult { IsValid = true, CardType = cardType };
        }

        private bool IsValidLuhn(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9)
                        n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }

        private string GetCardType(string cardNumber)
        {
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37"))
                return "AmEx";
            if (cardNumber.StartsWith("4"))
                return "Visa";
            if (cardNumber.StartsWith("22") || (cardNumber.StartsWith("51") && cardNumber.StartsWith("55")))
                return "Mastercard";
            if (cardNumber.StartsWith("6011"))
                return "Discover";

            return "Unknown";
        }
    }
}