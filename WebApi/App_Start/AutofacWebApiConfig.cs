using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DataAccessLayer.Repositories;
using DataAccessLayer.Persistence;
using Autofac.Integration.WebApi;
using System.Reflection;
using DataAccessLayer;
using System.Data.Entity;

namespace WebApi.IoC
{
    public class AutofacWebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            SetUpRegistration(builder);

            IContainer container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void SetUpRegistration(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<PrototipoConsultaUTNContext>().AsSelf();
        }
    }
}