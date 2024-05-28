using RpDev.Di.Installer.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace RpDev.Services.AudioService.Installer
{
    [CreateAssetMenu(menuName = "Installers/Audio Service Installer")]
    public class AudioServiceInstaller : VContainerScriptableObjectInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            BindService(builder);
        }

        private void BindService(IContainerBuilder builder)
        {
            builder.Register<AudioService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentOnNewGameObject<AudioServicePresenter>(Lifetime.Singleton,
                    "Audio Service Presenter")
                .DontDestroyOnLoad();
        }
    }
}