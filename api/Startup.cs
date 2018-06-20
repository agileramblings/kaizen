﻿using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using kaizen.domain.@base;
using kaizen.domain.@base.events;
using kaizen.domain.@base.events.storage;
using kaizen.domain.@base.messaging;
using kaizen.domain.@base.persistence;
using kaizen.domain.@base.Persistence;
using kaizen.domain.@base.utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace kaizen.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Kaizen API", Version = "v1" });
            });

            // Autofac
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacModule());

            // ReadModel Persistence
            builder.RegisterType<InMemoryReadModelStorage>()
                .As<IReadModelFacade>()
                .As<IReadModelPersistence>()
                .SingleInstance();

            // Agggregate Command/Event Persistence
            builder.RegisterType<AggregateRepository>().As<IAggregateRepository>().SingleInstance();
            builder.RegisterType<InMemoryCommandRepository>().As<ICommandRepository>().SingleInstance();
            builder.RegisterType<InMemoryEventDescriptorStorage>().As<IEventDescriptorStorage>().SingleInstance();
            builder.RegisterType<EventStore>().As<IEventStore>().SingleInstance();

            // Command/Event Handling
            builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>().SingleInstance();
            builder.RegisterType<EventHandlerFactory>().As<IEventHandlerFactory>().SingleInstance();

            // MessageBus (shared)
            builder.RegisterType<MessageBus>()
                .As<ICommandSender>()
                .As<IEventPublisher>()
                .SingleInstance();

            var container = builder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kaizen V1");
            });
            app.UseMvc();
        }
    }
}
