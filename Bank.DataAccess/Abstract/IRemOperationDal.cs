using System;
using System.Collections.Generic;
using System.Text;
using Bank.Entities.Concrete;

namespace Bank.DataAccess.Abstract
{
    public interface IRemOperationDal : IEntityRepository<RemOperation>
    {
    }
}
