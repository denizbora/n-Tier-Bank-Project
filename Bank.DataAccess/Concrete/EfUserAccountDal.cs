using System;
using System.Collections.Generic;
using System.Text;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.DataAccess.Concrete
{
    public class EfUserAccountDal : EfRepositoryBase<UserAccount, BankDbContext>, IUserAccountDal
    {
    }
}
