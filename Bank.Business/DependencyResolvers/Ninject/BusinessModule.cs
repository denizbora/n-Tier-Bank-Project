using System;
using System.Collections.Generic;
using System.Text;
using Bank.Business.Abstract;
using Bank.Business.Concrete;
using Bank.DataAccess.Abstract;
using Bank.DataAccess.Concrete;
using Ninject.Modules;

namespace Bank.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAccountService>().To<AccountManager>().InSingletonScope();
            Bind<IAccountDal>().To<EfAccountDal>().InSingletonScope();
            Bind<IActivityService>().To<ActivityManager>().InSingletonScope();
            Bind<IActivityDal>().To<EfActivityDal>().InSingletonScope();
            Bind<IRemOperationService>().To<RemOperationManager>().InSingletonScope();
            Bind<IRemOperationDal>().To<EfRemOperationDal>().InSingletonScope();
            Bind<IRemReceiverService>().To<RemReceiverManager>().InSingletonScope();
            Bind<IRemReceiverDal>().To<EfRemReceiverDal>().InSingletonScope();
            Bind<ISecQueService>().To<SecQueManager>().InSingletonScope();
            Bind<ISecQueDal>().To<EfSecQueDal>().InSingletonScope();
            Bind<IUserAccountService>().To<UserAccountManager>().InSingletonScope();
            Bind<IUserAccountDal>().To<EfUserAccountDal>().InSingletonScope();
        }
    }
}
