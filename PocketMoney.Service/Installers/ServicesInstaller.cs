﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PocketMoney.Data;
using PocketMoney.Service.Behaviors;
using PocketMoney.Service.Interfaces;

namespace PocketMoney.Service.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ProcessInterceptor>()
                    .Named("process.interceptor"));

            container.Register(
                Component.For<IMessageService>()
                    .ImplementedBy<MessageService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<ISettingService>()
                    .ImplementedBy<SettingService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<IFamilyService>()
                    .ImplementedBy<FamilyService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<ITaskService>()
                    .ImplementedBy<TaskService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<IGoalService>()
                    .ImplementedBy<GoalService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<IClientService>()
                    .ImplementedBy<ClientService>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);

            container.Register(
                Component.For<IConnector>()
                    .ImplementedBy<Connector>()
                    .Interceptors<ProcessInterceptor>()
                    .LifeStyle
                    .Singleton);
        }
    }
}
