using System.Reflection;
using Autofac;
using kaizen.domain.@base.messaging;
using kaizen.domain.retrospective;
using Module = Autofac.Module;

namespace kaizen.domain.@base.utilities
{
    // This is used to find all CommandHandlers and EventHandlers dynamically
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var asm = Assembly.GetExecutingAssembly();

            var assembliesToScan = new[]
            {
                typeof(Retrospective).GetTypeInfo().Assembly
                // add more assemblies as they are included in the service
            };

            foreach (var asm in assembliesToScan)
            {
                var handlerType = typeof(ICommandHandler<>);
                builder.RegisterAssemblyTypes(asm).AsClosedTypesOf(handlerType).SingleInstance().PropertiesAutowired();
                var handlerType2 = typeof(IHandles<>);
                builder.RegisterAssemblyTypes(asm).AsClosedTypesOf(handlerType2).SingleInstance().PropertiesAutowired();
            }
        }
    }
}