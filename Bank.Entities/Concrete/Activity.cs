using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Bank.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bank.Entities.Concrete
{
    public class Activity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string GovId { get; set; }
        public string Iban { get; set; }
        public string Event { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }


    }
}
