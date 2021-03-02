using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class RemReceiverManager : IRemReceiverService
    {
        IRemReceiverDal _remReceiverDal;

        public RemReceiverManager(IRemReceiverDal remReceiverDal)
        {
            _remReceiverDal = remReceiverDal;
        }
        public List<RemReceiver> GetAll(Expression<Func<RemReceiver, bool>> filter = null)
        {
            return _remReceiverDal.GetAll(filter);
        }

        public RemReceiver Get(Expression<Func<RemReceiver, bool>> filter)
        {
            return _remReceiverDal.Get(filter);
        }

        public void Add(RemReceiver entity)
        {
            _remReceiverDal.Add(entity);
        }

        public void Update(RemReceiver entity)
        {
            _remReceiverDal.Update(entity);
        }

        public void Delete(RemReceiver entity)
        {
            _remReceiverDal.Delete(entity);
        }
    }
}
