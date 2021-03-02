using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class RemOperationManager:IRemOperationService
    {
        IRemOperationDal _remOperationDal;

        public RemOperationManager(IRemOperationDal remOperationDal)
        {
            _remOperationDal = remOperationDal;
        }
        public List<RemOperation> GetAll(Expression<Func<RemOperation, bool>> filter = null)
        {
            return _remOperationDal.GetAll(filter);
        }

        public RemOperation Get(Expression<Func<RemOperation, bool>> filter)
        {
            return _remOperationDal.Get(filter);
        }

        public void Add(RemOperation entity)
        {
            _remOperationDal.Add(entity);
        }

        public void Update(RemOperation entity)
        {
            _remOperationDal.Update(entity);
        }

        public void Delete(RemOperation entity)
        {
            _remOperationDal.Delete(entity);
        }
    }
}
