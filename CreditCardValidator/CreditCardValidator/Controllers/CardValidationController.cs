using CreditCardValidator.Models;
using CreditCardValidatorAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace CreditCardValidator.Controllers
{
    [RoutePrefix("api/cardvalidation")]
    public class CardValidationController : ApiController
    {
        private readonly ICardValidationService _cardValidationService;

        public CardValidationController(ICardValidationService cardValidationService)
        {
            _cardValidationService = cardValidationService;
        }

        [HttpPost]
        [Route("validate")]
        public IHttpActionResult ValidateCard([FromBody] CardValidationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _cardValidationService.ValidateCard(request.CardNumber);
            return Ok(result);
        }
    }
}