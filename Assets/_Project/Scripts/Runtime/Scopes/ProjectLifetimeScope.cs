using UnityEngine;
using VContainer;
using VContainer.Unity;
using RpDev.Di.Installer.VContainer;
using RpDev.EntryPoint.Installer;
using RpDev.Gameplay.Installer;
using RpDev.GenericFactories.VContainer;
using RpDev.Level.Installer;
using RpDev.UserData;
using RpDev.Runtime.Screens;
using RpDev.Services;
using RpDev.Services.AssetProvider.Installers;
using RpDev.Services.AudioService;

namespace RpDev
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [Header("Scriptable object installer")]
        
        [SerializeField] private VContainerScriptableObjectInstaller[] _scriptableObjectInstallers;
        [SerializeField] private InputHandler _inputHandler;

        [Space] 
        [SerializeField] private LoadingScreen _loadingScreen;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterUser(builder);
            RegisterServices(builder);
            RegisterEntryPoint(builder);
            RegisterViews(builder);
            RegisterAudioHandler(builder);
            RegisterScriptableObjectInstallers(builder);
        }

        private void RegisterAudioHandler(IContainerBuilder builder)
        {
            builder.Register<GameAudioHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }

        private void RegisterEntryPoint(IContainerBuilder builder)
        {
            new AppCoreInstaller().Install(builder);
        }

        private void RegisterUser(IContainerBuilder builder)
        {
            new UserDataHandlerInstaller().Install(builder);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            new AssetProviderInstaller().Install(builder);
            new GenericFactoryInstaller().Install(builder);
            new GameplayInstaller().Install(builder);
            new LevelServiceInstaller().Install(builder);
            
            builder.Register<GamePreferences>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInNewPrefab(_inputHandler, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }

        private void RegisterViews(IContainerBuilder builder)
        {
            builder.RegisterInstance(_loadingScreen);
        }

        private void RegisterScriptableObjectInstallers(IContainerBuilder builder)
        {
            foreach (var installer in _scriptableObjectInstallers)
            {
                installer.Install(builder);
            }
        }

        //Non lazy calling
        private void Start()
        {
            Container.Resolve<InputHandler>();
            Container.Resolve<IAudioService>();
        }
    }
}