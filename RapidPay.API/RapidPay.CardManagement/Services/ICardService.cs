using RapidPay.State.Data;
using System.Threading.Tasks;

namespace RapidPay.CardManagement.Services
{
    public interface ICardService
    {
        public Task<CardResponse> CreateCard(CardRequest cardData);

        public Task<PaymentResponse> PaymentCard(PaymentRequest cardData);

        public Task<BalanceResponse> GetBalance(string cardNumber);
    }
}
