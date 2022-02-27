﻿using Autofac;
using PatPortal.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Infrastructure.Configuration
{
    public class ApplicationModule : Module
    {
        private readonly ApplicationConfiguration _setting;

        public ApplicationModule(ApplicationConfiguration setting)
        {
            _setting = setting;
        }
        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<DataAccessService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
