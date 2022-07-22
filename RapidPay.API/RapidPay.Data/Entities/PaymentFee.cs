using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.Data.Entities
{
    public class PaymentFee
    {
        public int PaymentFeeId { get; set; }
        public decimal Fee { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
