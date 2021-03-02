using System;
using System.Collections.Generic;
using System.Text;
using Bank.Entities.Concrete;

namespace Bank.DataAccess.Abstract
{
    public interface IActivityDal: IEntityRepository<Activity>
    {
    }
}
