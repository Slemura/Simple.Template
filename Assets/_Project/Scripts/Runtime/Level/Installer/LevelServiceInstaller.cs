using RpDev.Di.Installer.VContainer;
using RpDev.Level.Bootstrap;
using VContainer;

namespace RpDev.Level.Installer
{
    public class LevelServiceInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<LevelService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LevelBootstrap>(Lifetime.Transient).AsImplementedInterfaces();
        }
    }
}