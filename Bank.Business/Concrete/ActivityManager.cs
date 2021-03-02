using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Bank.Business.Abstract;
using Bank.DataAccess.Abstract;
using Bank.Entities.Concrete;

namespace Bank.Business.Concrete
{
    public class ActivityManager:IActivityService
    {
        IActivityDal _activityDal;

        public ActivityManager(IActivityDal activityDal)
        {
            _activityDal = activityDal;
        }
        public List<Activity> GetAll(Expression<Func<Activity, bool>> filter = null)
        {
            return _activityDal.GetAll(filter);
        }

        public Activity Get(Expression<Func<Activity, bool>> filter)
        {
            return _activityDal.Get(filter);
        }

        public void Add(Activity entity)
        {
            _activityDal.Add(entity);
        }

        public void Update(Activity entity)
        {
            _activityDal.Update(entity);
        }

        public void Delete(Activity entity)
        {
            _activityDal.Delete(entity);
        }
    }
}
