//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.ServiceModel;
//using System.Configuration;

//using EazyWizy.Framework.Configuration;

//namespace EazyWizy.Framework.Service
//{
//    public static class Factory
//    {
//        public static ChannelFactory<T> GetFactory<T>(string serviceName)
//        {
//            string usePTConfig = ConfigurationManager.AppSettings["UsePTConfig"];

//            if (!string.IsNullOrEmpty(usePTConfig) && (usePTConfig.ToLower().CompareTo("false") == 0))
//            {
//                return new ChannelFactory<T>(serviceName);
//            }
//            else
//            {
//                IConfigManager configManager = FrameworkContainer.Resolve<IConfigManager>();

//                string xmlString = configManager.GetXML("WCFService", serviceName, "R4");
//                ConfigurableServiceEndpoint configuration = new ConfigurableServiceEndpoint(typeof(T), xmlString);
//                return new ChannelFactory<T>(configuration);
//            }
//        }
//    }
//}
