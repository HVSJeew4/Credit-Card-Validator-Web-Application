namespace CreditCardValidator.Domain
{
    public class CardValidator
    {
        public (bool isValid, string cardType) ValidateCard(string cardNumber)
        {
            if (!IsNumeric(cardNumber)) return (false, "Invalid: Non-numeric characters");

            string cardType = GetCardType(cardNumber);
            if (cardType == "Unknown") return (false, "Unknown card type");

            bool isValid = IsLuhnValid(cardNumber);
            return (isValid, cardType);
        }

        private bool IsNumeric(string input) => input.All(char.IsDigit);

        private string GetCardType(string cardNumber)
        {
            if (cardNumber.StartsWith("34") || cardNumber.StartsWith("37") && cardNumber.Length == 15) return "AmEx";
            if (cardNumber.StartsWith("4") && cardNumber.Length == 16) return "Visa";
            if ((cardNumber.StartsWith("22") || cardNumber.StartsWith("51") || cardNumber.StartsWith("52") ||
                 cardNumber.StartsWith("53") || cardNumber.StartsWith("54") || cardNumber.StartsWith("55")) && cardNumber.Length == 16)
                return "Mastercard";
            if (cardNumber.StartsWith("6011") && cardNumber.Length == 16) return "Discover";
            return "Unknown";
        }

        private bool IsLuhnValid(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate) n *= 2;
                if (n > 9) n -= 9;
                sum += n;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }
    }
}
