using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        IAccountDal _accountDal;

        public AccountManager(IAccountDal accountDal)
        {
            _accountDal = accountDal;
        }
        public List<Account> GetAll(Expression<Func<Account, bool>> filter = null)
        {
            return _accountDal.GetAll(filter);
        }

        public Account Get(Expression<Func<Account, bool>> filter)
        {
            return _accountDal.Get(filter);
        }

        public void Add(Account entity)
        {
            _accountDal.Add(entity);
        }

        public void Update(Account entity)
        {
            _accountDal.Update(entity);
        }

        public void Delete(Account entity)
        {
            _accountDal.Delete(entity);
        }
    }
}
