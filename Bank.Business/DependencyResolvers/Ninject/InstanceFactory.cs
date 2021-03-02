using System;
using System.Collections.Generic;
using System.Text;
using Ninject;

namespace Bank.Business.DependencyResolvers.Ninject
{
    public class InstanceFactory
    {
        public static T GetInstance<T>()
        {
            return new StandardKernel(new BusinessModule()).Get<T>();
        }
    }
}
