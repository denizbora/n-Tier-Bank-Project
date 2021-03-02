using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bank.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bank.Entities.Concrete
{
    public class RemOperation : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string GovId { get; set; }
        public string RIban { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
        public string OpName { get; set; }
    }
}
