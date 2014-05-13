using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace EazyWizy.Framework.Service
{
    public class ConfigurableServiceEndpoint : ServiceEndpoint
    {
        private System.Configuration.Configuration _Config = null;
        private ServiceModelSectionGroup _ServiceModel
        {
            get
            {
                try
                {
                    return ServiceModelSectionGroup.GetSectionGroup(_Config);
                }
                catch { return null; }
            }
        }

        public ConfigurableServiceEndpoint(Type contractType, string serviceModelXML)
            : this(ContractDescription.GetContract(contractType), serviceModelXML)
        { }

        public ConfigurableServiceEndpoint(ContractDescription contract, string serviceModelXML)
            : base(contract)
        {
            FileInfo configurationFile = null;
            try
            {
                //Return if no binding settings are specified
                if (string.IsNullOrEmpty(serviceModelXML))
                    throw new ArgumentNullException();

                //create temporary file to store config
                string configFilePath = Path.GetTempFileName();
                configurationFile = new FileInfo(configFilePath);

                //overwrite file contents with config and lock writing to file
                using (var writer = new StreamWriter(configurationFile.Create()))
                {
                    writer.Write("<configuration>" + serviceModelXML + "</configuration>");
                    configurationFile.IsReadOnly = true;
                }

                //pointer to config file
                ExeConfigurationFileMap map = new ExeConfigurationFileMap()
                {
                    ExeConfigFilename = configFilePath
                };

                //load temp config file and get system.serviceModel section
                _Config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

                if (_ServiceModel == null)
                    throw new ArgumentException("Invalid serviceModelXML.");

                ChannelEndpointElement endpoint = null;

                foreach (ChannelEndpointElement ep in _ServiceModel.Client.Endpoints)
                {
                    // Find the first endpoint that is configured directly in our temp config file,
                    // because we may also be inheriting from the global config file.
                    if (ep.ElementInformation.IsPresent)
                    {
                        endpoint = ep;
                        break;
                    }
                }

                //validate endpoint
                if (endpoint == null)
                    throw new ArgumentException("No endpoints found.");
                if (endpoint.Address == null)
                    throw new ArgumentException("Invalid endpoint address.");

                this.Address = new EndpointAddress(endpoint.Address);

                _setBinding(endpoint.BindingConfiguration, endpoint.Binding);
                _setBehaviours();
            }
            finally
            {
                try
                {
                    //unlock configuration file so it may be deleted
                    if (configurationFile != null && configurationFile.IsReadOnly)
                        configurationFile.IsReadOnly = false;
                }
                catch { }

                //delete file which config file was temporarily stored in
                _deleteConfigFile();
            }
        }

        private void _setBinding(string bindingName, string bindingType)
        {
            if (string.IsNullOrEmpty(bindingName))
                throw new ArgumentNullException("Invalid binding name", "bindingName");
            if (string.IsNullOrEmpty(bindingType))
                throw new ArgumentNullException("Invalid binding type", "bindingType");
            if (_ServiceModel == null || _ServiceModel.Bindings == null)
                throw new ArgumentException("Invalid serviceModelXML.");

            //get valid binding from config section
            foreach (BindingCollectionElement bce in _ServiceModel.Bindings.BindingCollections.Where(b => b != null))
                foreach (IBindingConfigurationElement bind in bce.ConfiguredBindings.Where(b => b != null))
                    if (bind.Name == bindingName && bce.BindingName == bindingType)
                    {
                        try
                        {
                            //create binding of type BindingType
                            this.Binding = (Binding)bce.BindingType.GetConstructor(new Type[0]).Invoke(new object[0]);

                            //apply the configuration from the binding configuration element
                            bind.ApplyConfiguration(this.Binding);

                            this.Binding.Name = bind.Name;
                            return;
                        }
                        catch (NullReferenceException)
                        {
                            throw new Exception("Invalid binding configuration, no bindings of type " + bce.BindingType.Name + " with a parameterless constructor were found");
                        }
                        catch (Exception ee)
                        {
                            throw new Exception("Invalid binding configuration.\r\n" + ee.Message, ee);
                        }
                    }

            throw new Exception("Invalid binding configuration, binding \"" + bindingName + "\" was not found.");
        }

        private void _setBehaviours()
        {
            if (_ServiceModel == null || _ServiceModel.Behaviors == null || _ServiceModel.Behaviors.EndpointBehaviors == null)
                return;
            try
            {
                foreach (EndpointBehaviorElement behavior in _ServiceModel.Behaviors.EndpointBehaviors)
                    if (behavior != null)
                        foreach (BehaviorExtensionElement behaviorExtension in behavior.Where(b => b != null))
                        {
                            //this block uses reflection to call the protected abstract internal "CreateBehavior"
                            //method from each BehaviorExtensionElement
                            MethodInfo createBehavior = behaviorExtension.GetType().GetMethod("CreateBehavior", BindingFlags.NonPublic | BindingFlags.Instance);
                            IEndpointBehavior b = (IEndpointBehavior)createBehavior.Invoke(behaviorExtension, new object[0]);
                            if (b != null)
                                this.Behaviors.Add((IEndpointBehavior)b);
                        }
            }
            catch (Exception ee)
            {
                throw new InvalidOperationException("Unexpected error creating client endpoint behaviors", ee);
            }
        }

        private void _deleteConfigFile()
        {
            try
            {
                if (_Config != null && !string.IsNullOrEmpty(_Config.FilePath))
                    File.Delete(_Config.FilePath);
            }
            catch { }
        }

        ~ConfigurableServiceEndpoint()
        {
            _deleteConfigFile();
        }
    }
}
