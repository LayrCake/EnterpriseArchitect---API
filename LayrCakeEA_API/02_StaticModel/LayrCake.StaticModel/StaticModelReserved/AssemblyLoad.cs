using System;
using System.Diagnostics;
using LayrCake.StaticModel.StaticModelReserved;
using Infrastructure;
using Infrastructure.Core;
using Infrastructure.Globals;

namespace LayrCake.StaticModel
{
    [CommonAttributes.InitializeOnLoad]
    public class ClassAssemblyLoad
    {
        // Note - this must *not* have =false at the end,
        // otherwise it will be set to false every time the type
        // initializer is run, before the test for it being false.
        // We want the default value (false), but only the first time...
        static bool _initialized;
        static bool _assembliesLoaded = true;
        static bool _staticModelConfigured;
        static bool _castleWindsorLoaded = true;
        private static int _failCount;

        static internal string[] _assemblyList =
        {
            "AutoMapper", "Castle.Core", "Castle.Windsor", "ExpressionSerialization", "LinqKit"
        };

        static ClassAssemblyLoad()
        {
            do
            {
                ThreadAssemblyLoad();
                _failCount++;
                if (_failCount == 3) return;
            } while (!_assembliesLoaded && _staticModelConfigured && !_castleWindsorLoaded);
        }

        static void ThreadAssemblyLoad()
        {
            if (_assembliesLoaded && _staticModelConfigured && _castleWindsorLoaded)
            {
            }
            else
            {
                //if (!_assembliesLoaded)
                //{
                //    // In Debug mode the Core Assemblies are referenced therefore this will return the location of the Test assembly (Config items have not been pulled in)
                //    Configuration config = null;

                //    var assembly = typeof(ClassAssemblyLoad).Assembly;
                //    var assemblyName = assembly.GetName().Name;
                //    var assemblyPath = assembly.Location;
                //    config = ConfigSettings.GetCustomConfiguration(assemblyName, assemblyPath);
                //    if (!ConfigSettings.ConfigChecker(config))
                //    {
                //        assembly = Assembly.GetExecutingAssembly();
                //        config = ConfigSettings.GetConfigFile(assembly);
                //        if (!ConfigSettings.ConfigChecker(config))
                //        {
                //            assembly = Assembly.GetCallingAssembly();
                //            config = ConfigSettings.GetConfigFile(assembly);
                //        }
                //    }
                //    _assembliesLoaded = AssemblyLoad.Load(_assemblyList, true, config, true);

                //    if (!_assembliesLoaded)
                //    {
                //        Trace.Fail("ActionService Assembly Loader Failed.");
                //        EventLogging.WriteError("ActionService Assembly Loader Failed.",
                //            EventLoggingCodes.AssemblyLoadError.GetValue<int>());
                //        throw new ApplicationException("ActionService Assembly Loader Failed.");
                //    }
                //}

                //if (!_castleWindsorLoaded)
                //{
                //    _castleWindsorLoaded = new ClassAssemblyLoad().WindsorLoadServices();

                //    if (!_castleWindsorLoaded)
                //    {
                //        Trace.Fail("ActionService WindsorLoadServices.Configure() Failed.");
                //        EventLogging.WriteError("ActionService WindsorLoadServices.Configure() Failed.",
                //            EventLoggingCodes.DomainModelLoad.GetValue<int>());
                //        throw new ApplicationException("ActionService WindsorLoadServices.Configure() Failed.");
                //    }
                //}

                if (!_staticModelConfigured)
                {
                    _staticModelConfigured = StaticModelConfig.Configure();

                    if (!_staticModelConfigured)
                    {
                        Trace.Fail("ActionService ActionServiceConfig.Configure() Failed.");
                        EventLogging.WriteError("ActionService ActionServiceConfig.Configure() Failed.",
                            EventLoggingCodes.DomainModelLoad.GetValue<int>());
                        throw new ApplicationException("ActionService ActionServiceConfig.Configure() Failed.");
                    }
                }
            }
        }

        //public bool WindsorLoadServices()
        //{
        //    _containerWithServices = new WindsorContainer()
        //        .Install(new WindsorServiceInstaller());

        //    if (_containerWithServices != null)
        //        return true;
        //    else return false;
        //}
    }
}
