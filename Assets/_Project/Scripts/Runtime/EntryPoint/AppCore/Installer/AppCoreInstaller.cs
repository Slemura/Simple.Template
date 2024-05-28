using RpDev.Di.Installer.VContainer;
using RpDev.Services.AsyncStateMachine.Installer;
using VContainer;

namespace RpDev.EntryPoint.Installer
{
    public class AppCoreInstaller : VContainerInstaller
    {
        public override void Install(IContainerBuilder builder)
        {
            builder.Register<AppCore>(Lifetime.Singleton).AsImplementedInterfaces();
            RegisterStateMachine(builder);
        }
        
        private void RegisterStateMachine(IContainerBuilder builder)
        {
            new StateMachineInstaller().Install(builder);
        }
    }
}