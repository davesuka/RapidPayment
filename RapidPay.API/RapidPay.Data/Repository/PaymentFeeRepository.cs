using Microsoft.EntityFrameworkCore;
using RapidPay.Data.Context;
using RapidPay.Data.Entities;
using RapidPay.State.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public class PaymentFeeRepository : IPaymentFeeRepository
    {
        private readonly DataContext _context;
        private readonly ICardRepository _cardRepository;

        public PaymentFeeRepository(DataContext context, ICardRepository cardRepository)
        {
            _context = context;
            _cardRepository = cardRepository;
        }

        public async Task<bool> AddPaymentFee(decimal fee)
        {
            var paymentFee = new PaymentFee
            {
                Fee = fee,
                UpdatedDate = DateTime.Now
            };

            _context.PaymentFee.Add(paymentFee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;            
            
        }

        public async Task<bool> AddPaymentHistory(PaymentRequest paymentRequest, decimal fee)
        {
            var card = await _cardRepository.GetCard(paymentRequest.Number);

            if (card == null)
                return false;

            var paymentHistory = new PaymentHistory
            {
                PaymentAmount = paymentRequest.Amount,
                PaymentDate = DateTime.Now,
                PaymentFee = fee,
                CardId = card.CardId
            };

            _context.PaymentHistory.Add(paymentHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<(decimal, DateTime)> GetLastFee()
        {
            var paymentFee = 
                await _context.PaymentFee.OrderBy(fee => fee.UpdatedDate).Take(1).FirstOrDefaultAsync();
            
            if (paymentFee == null)
            {
                return default;
            }
            return (paymentFee.Fee, paymentFee.UpdatedDate);
        }
    }
}
