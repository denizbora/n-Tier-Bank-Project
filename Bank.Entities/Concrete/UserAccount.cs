using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Bank.Entities.Abstract;

namespace Bank.Entities.Concrete
{
    public class UserAccount : IEntity
    {
        [Key]
        public string GovId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SecQue { get; set; }
        public string SecAns { get; set; }
    }
}
