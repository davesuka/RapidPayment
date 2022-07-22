using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.State.Data
{
    public class PaymentResponse: BalanceResponse
    {
        public decimal Fees { get; set; }
    }
}
