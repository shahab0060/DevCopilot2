using Autofac;
using DevCopilot2.Core.Services.Interfaces;
using System.Reflection;

namespace DevCopilot2.API.Dependencies
{
    public class DependencyContainer
    {
        public static void RegisterService(ContainerBuilder builder)
        {
            string assemblyName = typeof(DependencyContainer).FullName!.Split('.')[0];
            string assemblyName2 = typeof(IUserService).FullName!.Split('.')[0];
            bool equal = string.Equals(assemblyName, assemblyName2);
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var ourProjectAssemblies = allAssemblies
              .Where(x => x.FullName!.StartsWith(assemblyName));
            //var services = ourProjectAssemblies
            //   .Where(t => t.FullName!.EndsWith("Service"));
            //var repositories = ourProjectAssemblies
            //  .Where(t => t.FullName!.EndsWith("Repository"));

            //RegisterAssemblyTypes(builder, services.ToArray());
            //RegisterAssemblyTypes(builder, repositories.ToArray());

            builder.RegisterAssemblyTypes(ourProjectAssemblies.ToArray())
               .Where(t => (t.IsClass || t.IsInterface) && t.FullName.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();


            //builder.RegisterAssemblyTypes(ourProjectAssemblies.ToArray())
            //   .Where(t => (t.IsClass || t.IsInterface) && t.FullName.EndsWith("Repository"))
            //   .AsImplementedInterfaces()
            //   .InstancePerLifetimeScope();

        }

        static void RegisterAssemblyTypes(ContainerBuilder builder, Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies.ToArray())
               .Where(t => (t.IsClass || t.IsInterface) && t.FullName.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
    }
}
