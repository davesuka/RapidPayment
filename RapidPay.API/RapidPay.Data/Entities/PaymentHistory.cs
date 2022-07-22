using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.Data.Entities
{
    public class PaymentHistory
    {
        public int PaymentHistoryId { get; set; }
        public decimal PaymentAmount { get; set; }
        public decimal PaymentFee { get; set; }
        public DateTime PaymentDate { get; set; }
        public int CardId { get; set; }
        public Card Card { get; set; }
    }
}
