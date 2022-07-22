using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.State.Data
{
    public class PaymentRequest
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
    }
}
