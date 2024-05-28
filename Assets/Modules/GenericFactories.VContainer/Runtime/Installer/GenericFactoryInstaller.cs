using RpDev.Di.Installer.VContainer;
using RpDev.Services.GenericFactories.VContainer;
using VContainer;

namespace RpDev.GenericFactories.VContainer
{
    public class GenericFactoryInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<PlainClassFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GOFactory>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}