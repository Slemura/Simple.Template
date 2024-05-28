using System;
using System.Collections.Generic;
using RpDev.Runtime.Screens;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using RpDev.UI.Handlers;
using VContainer.Unity;

namespace RpDev.Gameplay.UI
{
    public class GameOverScreenMediator : UIMediatorBase<GameOverScreen>, IInitializable, IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();
        private readonly IPlainClassFactory _plainClassFactory;
        
        private MusicButtonHandler _musicButtonHandler;
        private SoundButtonHandler _soundButtonHandler;

        private GameOverScreenMediator(IPlainClassFactory plainClassFactory)
        {
            _plainClassFactory = plainClassFactory;
        }
        
        public void Initialize()
        {
            _musicButtonHandler = _plainClassFactory.Create<MusicButtonHandler>();
            _musicButtonHandler.AddMusicButtonView(View.MusicButton);
            
            _soundButtonHandler = _plainClassFactory.Create<SoundButtonHandler>();
            _soundButtonHandler.AddSoundButtonView(View.SoundButton);
            
            _disposables.Push(_soundButtonHandler);
            _disposables.Push(_musicButtonHandler);
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
            {
                _disposables.Pop().Dispose();
            }
        }
    }
}
