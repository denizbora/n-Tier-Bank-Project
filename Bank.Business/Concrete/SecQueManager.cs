using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class SecQueManager:ISecQueService
    { 
        ISecQueDal _secQueDal;

        public SecQueManager(ISecQueDal secQueDal)
        {
            _secQueDal = secQueDal;
        }
        public List<SecQue> GetAll(Expression<Func<SecQue, bool>> filter = null)
        {
            return _secQueDal.GetAll(filter);
        }

        public SecQue Get(Expression<Func<SecQue, bool>> filter)
        {
            return _secQueDal.Get(filter);
        }

        public void Add(SecQue entity)
        {
            _secQueDal.Add(entity);
        }

        public void Update(SecQue entity)
        {
            _secQueDal.Update(entity);
        }

        public void Delete(SecQue entity)
        {
            _secQueDal.Delete(entity);
        }
    }
}
