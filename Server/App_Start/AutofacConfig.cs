using Autofac;
using Autofac.Integration.WebApi;
using Server.Infrastructure;
using Server.Models;
using System.Reflection;
using System.Web.Http;

namespace Server
{
    public class AutofacConfig
    {
        internal static void Register(HttpConfiguration httpConfiguration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<HotelsCityProvider>()
                .As<IHotelsProvider>()
                .SingleInstance();

            builder.RegisterType<HotelsCoastProvider>()
                .As<IHotelsProvider>()
                .SingleInstance();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}