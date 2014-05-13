using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using EazyWizy.Framework.Caching;
using EazyWizy.Framework.Configuration;
using System.Reflection;

namespace EazyWizy.Framework
{
    /// <summary>
    /// Container used to create and construct configured EazyWizy.Framework objects using Unity.
    /// </summary>
    public static class FrameworkContainer
    {
        private static volatile bool _containerIsConfigured = false;
        private readonly static object _lock = new object();
        private static IUnityContainer _container;

        internal static IUnityContainer Current
        {
            get
            {
                if (!_containerIsConfigured)
                {
                    lock (_lock)
                    {
                        if (!_containerIsConfigured)
                        {
                            InitializeContainer();
                            _containerIsConfigured = true;  // Set only when object is fully initialized
                        }
                    }
                }

                return _container;
            }
        }

        /// <summary>
        /// Create an instance of an object configured through the container.
        /// </summary>
        /// <typeparam name="T">Type for which to create an instance</typeparam>
        /// <returns>Object instance</returns>
        public static T Resolve<T>()
        {
            return Resolve<T>(null);
        }

        /// <summary>
        /// Create an instance of an object configured through the container.
        /// </summary>
        /// <typeparam name="T">Type for which to create an instance</typeparam>
        /// <param name="name">Name of the instance of the requested object</param>
        /// <returns>Object instance</returns>
        public static T Resolve<T>(string name)
        {
            IUnityContainer container = Current;

            lock (_lock)
            {
                if (!container.IsRegistered<T>(name))
                {
                    RegisterType<T>(container, name);
                }

                return container.Resolve<T>(name);
            }
        }

        private static void InitializeContainer()
        {
            _container = new UnityContainer();

            // An application may want to override the default registrations.  For instance, a unit test might want to
            // replace the implementation of IConfigManager with one that does not access a database.
            // Try to load the unity section from the default config file.  If it exists, look for a container config
            // with a name of EazyWizyFramework.  If it exists, configure the container with it.
            UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            if (section != null)
            {
                var container = section.Containers.Where(x => (string.Compare(x.Name, "EazyWizyFramework", true) == 0)).FirstOrDefault();
                if (container != null)
                {
                    _container.LoadConfiguration(section, "EazyWizyFramework");
                }
            }

            if (!_container.IsRegistered<IConfigManager>(null))
            {
                // Register a default type mapping for IConfigManager
                IContainerRegistration cr1 = new ConfigManagerContainerRegistrar();
                cr1.Register(_container, null);
            }

            if (!_container.IsRegistered<ICacheManager>(null))
            {
                // Register a default type mapping for ICacheManager
                IContainerRegistration cr2 = new CacheManagerContainerRegistrar();
                cr2.Register(_container, null);
            }
        }

        private static void RegisterType<T>(IUnityContainer container, string name)
        {
            if (typeof(T).Equals(typeof(IConfigManager)))
            {
                IContainerRegistration cr = new ConfigManagerContainerRegistrar();
                cr.Register(container, name);
            }

            else if (typeof(T).Equals(typeof(ICacheManager)))
            {
                IContainerRegistration cr = new CacheManagerContainerRegistrar();
                cr.Register(container, name);
            }
            else
            {
                string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(4);

                Type logWriterType =
                    //Type.GetType("EazyWizy.Framework.Logging.ILogWriter, EazyWizy.Framework.Logging, Version=" + assemblyVersion + ", Culture=neutral, PublicKeyToken=11b6d370641721ed", true);
                    Type.GetType("EazyWizy.Framework.Logging.ILogWriter, EazyWizy.Framework.Logging, Version=" + assemblyVersion + ", Culture=neutral", true);

                if (typeof(T).Equals(logWriterType))
                {
                    Type nlogLogWriterType = Type.GetType("EazyWizy.Framework.Logging.LogWriterContainerRegistrar, EazyWizy.Framework.Logging, Version=" + assemblyVersion + ", Culture=neutral", true);

                    IContainerRegistration cr = Activator.CreateInstance(nlogLogWriterType) as IContainerRegistration;
                    cr.Register(container, name);
                }
            }
        }
    }
}
