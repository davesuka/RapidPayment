using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RapidPay.Data.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string Number { get; set; }
        [Column(TypeName = "decimal(15,4)")]
        public decimal Balance { get; set; }
        public ICollection<PaymentHistory> PaymentHistories { get; set; }
    }
}
