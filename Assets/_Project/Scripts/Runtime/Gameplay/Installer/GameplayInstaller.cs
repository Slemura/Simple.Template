using RpDev.Di.Installer.VContainer;
using RpDev.Gameplay.Model;
using VContainer;

namespace RpDev.Gameplay.Installer
{
    public class GameplayInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<GameplayModel>(Lifetime.Singleton).AsSelf();
        }
    }
}