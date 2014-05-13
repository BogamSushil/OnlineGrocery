using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace EazyWizy.Framework.Caching
{
    internal class CacheManagerContainerRegistrar : IContainerRegistration
    {
        public void Register(IUnityContainer container, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                container.RegisterType<ICacheManager, NetFxCacheManager>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            }
            else
            {
                container.RegisterType<ICacheManager, NetFxCacheManager>(name, new ContainerControlledLifetimeManager(), new InjectionConstructor(name));
            }
        }
    }
}
