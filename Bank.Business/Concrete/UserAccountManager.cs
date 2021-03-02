using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class UserAccountManager : IUserAccountService
    {
        IUserAccountDal _userAccountDal;

        public UserAccountManager(IUserAccountDal userAccountDal)
        {
            _userAccountDal = userAccountDal;
        }
        public List<UserAccount> GetAll(Expression<Func<UserAccount, bool>> filter = null)
        {
            return _userAccountDal.GetAll(filter);
        }

        public UserAccount Get(Expression<Func<UserAccount, bool>> filter)
        {
            return _userAccountDal.Get(filter);
        }

        public void Add(UserAccount entity)
        {
            _userAccountDal.Add(entity);
        }

        public void Update(UserAccount entity)
        {
            _userAccountDal.Update(entity);
        }

        public void Delete(UserAccount entity)
        {
            _userAccountDal.Delete(entity);
        }
    }
}
