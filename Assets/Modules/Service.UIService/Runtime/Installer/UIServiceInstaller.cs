using RpDev.Di.Installer.VContainer;
using RpDev.Services.UI.Mediators;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace RpDev.Services.UI.Installer
{
    [CreateAssetMenu(menuName = "Services/UIServiceInstaller" )]
    public class UIServiceInstaller : VContainerScriptableObjectInstaller
    {
        [SerializeField] private UIServicePresenter _uiServiceRoot;
        
        public override void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiServiceRoot, Lifetime.Singleton)
                .DontDestroyOnLoad();

            builder.Register<UIService>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<UIScreenFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<UIMediatorFactory>(Lifetime.Singleton).AsSelf();
        }
    }
}