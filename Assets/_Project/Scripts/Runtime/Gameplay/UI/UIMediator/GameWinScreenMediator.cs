using System;
using System.Collections.Generic;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using RpDev.UI.Handlers;
using VContainer.Unity;

namespace RpDev.Gameplay.UI
{
    public class GameWinScreenMediator : UIMediatorBase<GameWinScreen>, IInitializable, IDisposable
    {
        private readonly Stack<IDisposable> _disposables = new();
        private readonly IPlainClassFactory _plainClassFactory;

        private GameWinScreenMediator(IPlainClassFactory plainClassFactory)
        {
            _plainClassFactory = plainClassFactory;
        }

        public void Initialize()
        {
            var musicButtonHandler = _plainClassFactory.Create<MusicButtonHandler>();
            musicButtonHandler.AddMusicButtonView(View.MusicButton);
            
            var soundButtonHandler = _plainClassFactory.Create<SoundButtonHandler>();
            soundButtonHandler.AddSoundButtonView(View.SoundButton);
            
            
            _disposables.Push(musicButtonHandler);
            _disposables.Push(soundButtonHandler);
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