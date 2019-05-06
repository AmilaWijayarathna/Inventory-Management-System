﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NHibernate;
using System.Reflection;
using Galaxy.Base.Data.DAL;
using Galaxy.Base.Service;
using Galaxy.Base.Domain;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using Castle.Core;
using FluentNHibernate.Automapping;
using Galaxy.Base.Data.Convertions;
using Galaxy.Base.Data.Mapping;
using NHibernate.Tool.hbm2ddl;

namespace DependencyInjection
{
   public class DependencyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.ComponentRegistered += Kernel_ComponentRegistered;

            //Register all controllers
            container.Register(

                //Nhibernate session factory
                Component.For<ISessionFactory>().UsingFactoryMethod(CreateSessionFactory).LifeStyle.Singleton,

                //Unitofwork interceptor
                Component.For<UnitOfWorkInterceptor>().LifeStyle.Transient,

                //All repoistories
                Classes.FromAssembly(Assembly.GetAssembly(typeof(Repository<>))).InSameNamespaceAs(typeof(Repository<>)).WithService.DefaultInterfaces().LifestyleTransient(),

                //All services
                Classes.FromAssembly(Assembly.GetAssembly(typeof(Service<>))).InSameNamespaceAs(typeof(Service<>)).WithService.DefaultInterfaces().LifestyleTransient()
               
                //All controllers
             //   Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient()

                );
        }


       
          public static ISessionFactory CreateSessionFactory()
        {
            string ConnectionString = "Data Source=T-SAFARI;Initial Catalog=testInventoryDB;User ID=sa;Password=s@123456";

            var cfgi = new StoreConfiguration();

            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(ConnectionString)
                    .ShowSql()
                );
            var configuration =
                fluentConfiguration.Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<BaseClass>(cfgi).
                        UseOverridesFromAssemblyOf<BaseClass>().Conventions.Add(typeof(CustomIdConvention))).
                    Add(AutoMap.AssemblyOf<Document>(cfgi)));
            ISessionFactory sessionFactory = configuration.ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(true, true);
                    //  new SchemaExport(cfg).Create(true, true);
                })
                .BuildSessionFactory();
            return sessionFactory;
        }
         

        void Kernel_ComponentRegistered(string key, Castle.MicroKernel.IHandler handler)
        {

            if (UnitOfWorkHelper.IsRepositoryClass(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            foreach (var method in handler.ComponentModel.Implementation.GetMethods())
            {
                if (UnitOfWorkHelper.HasUnitOfWorkAttribute(method))
                {
                    handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
                    return;
                }
            }
        }
    }
}
