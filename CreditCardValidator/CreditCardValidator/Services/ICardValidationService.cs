using CreditCardValidator.Models;

namespace CreditCardValidatorAPI.Services
{
    public interface ICardValidationService
    {
        CardValidationResult ValidateCard(string cardNumber);
    }
}