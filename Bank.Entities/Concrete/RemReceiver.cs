using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bank.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bank.Entities.Concrete
{
    public class RemReceiver : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string GovId { get; set; }
        public string RIban { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
    }
}
