using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public  class IocContainer
    {
        private static IUnityContainer _container;

        static IocContainer()
        {
            try
            {
                _container = new UnityContainer();

                UnityConfigurationSection section = (UnityConfigurationSection)System.Configuration.ConfigurationManager.GetSection("unity");

                _container.LoadConfiguration(section);
                _container.AddExtension(new EnterpriseLibraryCoreExtension());
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static IUnityContainer Instance
        {
            get { return _container; }
        }

        public static T Resolve<T>(params ResolverOverride[] overrides)
        {
            return _container.Resolve<T>(overrides);
        }

        public static T Resolve<T>(string name)
        {
            return _container.Resolve<T>(name);
        }
    }
    
}
