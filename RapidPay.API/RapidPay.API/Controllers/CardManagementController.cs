using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RapidPay.CardManagement.Services;
using RapidPay.State.Data;
using System.Threading.Tasks;

namespace RapidPay.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardManagementController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardManagementController> _logger;

        public CardManagementController(ICardService cardService, 
            ILogger<CardManagementController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }


        /// <summary>
        /// CreatesCard method
        /// </summary>
        /// <param name="request">include the cardnumber and the balance</param>
        /// <returns>status code withind the cardnumber created</returns>
        [HttpPost("add")]
        public async Task<ActionResult<CardResponse>> CreateCardAsync(CardRequest request)
        {
            if (string.IsNullOrEmpty(request.Number) || request.Number.Length != 15 || request.Balance <= 0)
            {
                return BadRequest();
            }

            try
            {
                var response = await _cardService.CreateCard(request);
                return Ok(response.Number);
            }
            catch (System.Exception ex)
            {
                var logError = $"CreatesCard: {request.Number}. Error: {ex.Message}";
                _logger.LogError(logError, ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
            }

        }

        [HttpGet("{cardNumber}")]       
        public async Task<ActionResult<CardResponse>> GetCardBalanceAsync(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 15)
            {
                return BadRequest();
            }
            try
            {
                var card = await _cardService.GetBalance(cardNumber);
                if (card == null)
                {
                    return NotFound(new { message = "The Card does not exist" });
                }

                return Ok(card);
            }
            catch (System.Exception ex)
            {
                var logError = $"GetCardBalance: {cardNumber}. Error: {ex.Message}";
                _logger.LogError(logError, ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
            }

        }

        [HttpPut("payment")]
        public async Task<ActionResult<PaymentResponse>> PaymentAsync(PaymentRequest paymentRequest)
        {
            if (string.IsNullOrEmpty(paymentRequest.Number) || paymentRequest.Number.Length != 15 || paymentRequest.Amount <= 0)
            {
                return BadRequest();
            }

            PaymentResponse response;
            try
            {
                response = await _cardService.PaymentCard(paymentRequest);
            }
            catch (System.Exception ex)
            {
                var logError =
                    $"CardPayment Number: {paymentRequest.Number} " +
                    $"amount: {paymentRequest.Amount}. " +
                    $"Error: {ex.Message}";
                _logger.LogError(logError, ex);
                return this.StatusCode(StatusCodes.Status500InternalServerError, logError);
            }

            return Ok(response);
        }
    }
}
