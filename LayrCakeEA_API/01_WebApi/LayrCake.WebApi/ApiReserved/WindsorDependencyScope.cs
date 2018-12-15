using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Lifestyle;
using System.Web.Http;
using LayrCake.StaticModel.StaticModelReserved;
using LayrCake.StaticModel.Repositories;
using LayrCake.WebApi.FakeControllers;
using LayrCake.WebApi.Controllers;

namespace LayrCake.WebApi.ApiReserved
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
                return this._container.Resolve(serviceType);
            else
                return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }

    public class WindsorDependencyScope : IDependencyScope
    {
        private readonly IWindsorContainer _container;
        private readonly IDisposable _scope;

        public WindsorDependencyScope(IWindsorContainer container)
        {
            this._container = container;
            this._scope = container.BeginScope();
        }

        public object GetService(Type serviceType)
        {
            if (_container.Kernel.HasComponent(serviceType))
                return _container.Resolve(serviceType);
            else
                return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this._container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            this._scope.Dispose();
        }
    }

    public class ApiControllersInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IRepository<>))
                        .ImplementedBy(typeof(IRepository<>))
                        .LifeStyle.PerWebRequest,

                Classes.FromAssemblyNamed("LayrCake.StaticModel")
                    .Where(type => type.Name.EndsWith("Repository"))
                    .WithServiceAllInterfaces()
                    .LifestylePerWebRequest());

            //container.Register(
            //    Component.For<ApiController>()
            //            .ImplementedBy<FakeChallengeController>().IsDefault()
            //            .LifeStyle.PerWebRequest);

            //container.Register(
            //    Component.For(typeof(ApiController))
            //            .ImplementedBy(typeof(ITableController<>))
            //            .LifeStyle.PerWebRequest);

            container.Register(Classes.FromThisAssembly()
                 .BasedOn<ApiController>()
                 .LifestylePerWebRequest());

            //container.Register(
            //    Component.For<ITableController<Challenge>>()
            //            .ImplementedBy<FakeChallengeController>()
            //            .LifeStyle.PerWebRequest);
        }
    }
}