using RapidPay.State.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Data.Repository
{
    public interface IPaymentFeeRepository
    {
        Task<bool> AddPaymentHistory(PaymentRequest paymentRequest, decimal fee);

        Task<bool> AddPaymentFee(decimal fee);
        Task<(decimal, DateTime)> GetLastFee();
    }
}
