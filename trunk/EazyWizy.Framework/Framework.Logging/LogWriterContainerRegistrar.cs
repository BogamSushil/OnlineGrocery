using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using EazyWizy.Framework.Caching;
using EazyWizy.Framework.Configuration;
using EazyWizy.Framework.Logging.NLog;

namespace EazyWizy.Framework.Logging
{
    internal class LogWriterContainerRegistrar : IContainerRegistration
    {
        public void Register(IUnityContainer container, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                container.RegisterType<ILogWriter, NLogLogWriter>(new ContainerControlledLifetimeManager(), new InjectionConstructor(typeof(IConfigManager)));
            }
            else
            {
                container.RegisterType<ILogWriter, NLogLogWriter>(name, new ContainerControlledLifetimeManager(), new InjectionConstructor(typeof(IConfigManager), name));
            }
        }
    }
}
