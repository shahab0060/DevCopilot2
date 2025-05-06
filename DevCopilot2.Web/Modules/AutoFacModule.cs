using Autofac;
using DevCopilot2.Web.PresentationExtensions;
using DevCopilot2.DataLayer.Context;
using DevCopilot2.IOC.Dependencies;

namespace DevCopilot2.Web.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();
            builder.RegisterContext<DevCopilot2DbContext>(ConnectionStringNames.Core);
            DependencyContainer.RegisterService(builder);
        }
    }
}
