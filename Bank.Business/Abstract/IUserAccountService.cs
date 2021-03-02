using System;
using System.Collections.Generic;
using System.Text;
using Bank.Entities.Concrete;

namespace Bank.Business.Abstract
{
    public interface IUserAccountService : IServiceRepository<UserAccount>
    {
    }
}
