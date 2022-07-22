using RapidPay.Data.Repository;
using RapidPay.State.Data;
using RapidPay.Utilities;
using System;
using System.Threading.Tasks;

namespace RapidPay.CardManagement.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IPaymentFeeRepository _paymentFeeRepository;

        public CardService(ICardRepository cardRepository, IPaymentFeeRepository paymentFeeRepository)
        {
            _cardRepository = cardRepository;
            _paymentFeeRepository = paymentFeeRepository;
        }

        public async Task<CardResponse> CreateCard(CardRequest cardData)
        {
            return await _cardRepository.CreateNewCard(cardData);
        }

        public async Task<BalanceResponse> GetBalance(string cardNumber)
        {
            decimal? balance = await _cardRepository.GetBalance(cardNumber);

            if(!balance.HasValue)
            {
                return null;
            }

            return new BalanceResponse { Number = cardNumber, Balance = balance.Value };

        }

        public async Task<PaymentResponse> PaymentCard(PaymentRequest request)
        {  
            var currentBalance = await GetBalance(request.Number);
            if (currentBalance == null)
            {
                throw new System.Exception("There is not enough balance in the card");
            }

            var feeToPay = await Utils.Instance.FeedCalculator(_paymentFeeRepository);

            var totalAmountToPay = request.Amount + feeToPay;
            if (currentBalance.Balance - totalAmountToPay < 0)
            {
                throw new System.Exception("There is not enough balance to apply the payment");
            }

            var response = new PaymentResponse { Number = request.Number };            

            if(await _paymentFeeRepository.AddPaymentHistory(request, feeToPay))
            {
                var updatedBalance = await _cardRepository.UpdateBalance(request.Number, totalAmountToPay);
                response.Balance = updatedBalance;
                response.Fees = feeToPay;
            }

            return response;
        }
    }
}
