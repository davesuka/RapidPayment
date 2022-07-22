using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RapidPay.State.Data
{
    public class CardRequest
    {
        [Required]
        public string Number { get; set; }

        public decimal Balance { get; set; }

    }
}
