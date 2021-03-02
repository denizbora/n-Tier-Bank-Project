using System;
using System.Collections.Generic;
using System.Text;
using Bank.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Bank.Entities.Concrete
{
    [Keyless]
    public class SecQue : IEntity
    {
        public string Question { get; set; }
    }
}
