using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bank.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bank.Entities.Concrete
{
    
    public class Account : IEntity
    {
        [Key]
        public string Iban { get; set; }
        public string GovId { get; set; }
        public string AccName { get; set; }
        public decimal Funds { get; set; }
        public string Date { get; set; }
        public string Currency { get; set; }
    }
}
