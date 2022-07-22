using RapidPay.Data.Entities;
using RapidPay.State.Data;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public interface ICardRepository
    {
        Task<CardResponse> CreateNewCard(CardRequest request);

        Task<decimal?> GetBalance(string cardNumber);
        Task<PaymentResponse> CardPayment(PaymentRequest request);
        Task<decimal> UpdateBalance(string cardNumber, decimal amount);
        Task<Card> GetCard(string cardNumber);
    }
}
