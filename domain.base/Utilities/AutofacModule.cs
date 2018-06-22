using System.Reflection;
using Autofac;
using kaizen.domain.@base.messaging;
using Module = Autofac.Module;

namespace kaizen.domain.@base.utilities
{
    // This is used to find all CommandHandlers and EventHandlers dynamically
    public class AutofacExampleModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Load multiple assemblies to scan for handlers
            var assembliesToScan = new[]
            {
                typeof(Command).GetTypeInfo().Assembly
                // add more assemblies as they are included in the service
            };
            // -- or single assembly --
            var asm = Assembly.GetExecutingAssembly();

            var handlerType = typeof(ICommandHandler<>);
            builder.RegisterAssemblyTypes(asm).AsClosedTypesOf(handlerType).SingleInstance().PropertiesAutowired();
            var handlerType2 = typeof(IHandles<>);
            builder.RegisterAssemblyTypes(asm).AsClosedTypesOf(handlerType2).SingleInstance().PropertiesAutowired();
        }
    }
}