using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Context;
using RapidPay.Data.Entities;
using RapidPay.State.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public class CardRepository : ICardRepository
    {
        private readonly DataContext _context;

        public CardRepository(DataContext context)
        {
            _context = context;
        }        

        public async Task<CardResponse> CreateNewCard(CardRequest request)
        {
            var newCard = new Card
            {
                Number = request.Number,
                Balance = request.Balance
            };
            _context.Add(newCard);
            await _context.SaveChangesAsync();
            return new CardResponse() { CardId = newCard.CardId, Number = newCard.Number };
        }

        public async Task<decimal?> GetBalance(string cardNumber)
        {
            var card = await GetCard(cardNumber);
            if(card == null)
            {
                return null;
            }
            return card.Balance;            
        }

        public async Task<PaymentResponse> CardPayment(PaymentRequest request)
        {
            var card = await GetCard(request.Number);
            if (card == null)
            {
                return null;
            }
            return new PaymentResponse { Balance = card.Balance };

        }

        public async Task<Card> GetCard(string cardNumber)
        {
            return await _context.Card.FirstOrDefaultAsync(card => card.Number == cardNumber);
        }

        public async Task<decimal> UpdateBalance(string cardNumber, decimal amount)
        {
            var card = await GetCard(cardNumber);
            if (card == null)
            {
                return 0;
            }
            
            decimal updatedBalance = card.Balance - amount;
            card.Balance = updatedBalance;
            await _context.SaveChangesAsync();
            return updatedBalance;
        }
    }
}
