using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace EazyWizy.Framework.Configuration
{
    internal class ConfigManagerContainerRegistrar : IContainerRegistration
    {
        public void Register(IUnityContainer container, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                container.RegisterType<IConfigManager, ConfigManager>(new ContainerControlledLifetimeManager(), new InjectionConstructor());
            }
        }
    }
}
