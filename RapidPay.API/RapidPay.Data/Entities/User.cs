using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RapidPay.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string UserName { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }
    }
}
