using Autofac;
using DevCopilot2.API.Dependencies;

namespace DevCopilot2.API.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            //builder.RegisterContext<DevCopilot2DbContext>(ConnectionStringNames.Core);
            DependencyContainer.RegisterService(builder);
        }
    }
}
