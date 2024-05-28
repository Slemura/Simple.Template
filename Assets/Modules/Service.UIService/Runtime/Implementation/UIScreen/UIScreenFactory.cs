using Cysharp.Threading.Tasks;
using RpDev.Extensions.Unity;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Utils;
using UnityEngine;

namespace RpDev.Services.UI
{
    public class UIScreenFactory
    {
        private readonly IGOFactory _goFactory;
        private UIScreenPrototypeProvider _prototypeProvider;

        public UIScreenFactory(IGOFactory goFactory)
        {
            _goFactory = goFactory;
        }
        
        internal async UniTask LoadPrototypes()
        {
            _prototypeProvider = new UIScreenPrototypeProvider();
            await _prototypeProvider.LoadScreenPrototypesAsync();
        }
        
        internal  TScreen CreateScreen<TScreen> (Transform root) where TScreen : UIScreen
        {
            var prototype = _prototypeProvider.GetPrototype<TScreen>();
            var screen = _goFactory.Create(prototype, root).WithGameObjectName(prototype.name);
            return (TScreen)screen;
        }
    }
}