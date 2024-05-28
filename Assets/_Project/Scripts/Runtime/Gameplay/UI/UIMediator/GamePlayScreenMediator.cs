using System;
using System.Collections.Generic;
using RpDev.Gameplay.Model;
using RpDev.Runtime.Screens;
using RpDev.Services.GenericFactories.VContainer;
using RpDev.Services.UI.Mediators;
using RpDev.UI.Handlers;
using UnityEngine;
using VContainer.Unity;

namespace RpDev.Gameplay.UI
{
    public class GamePlayScreenMediator : UIMediatorBase<GamePlayScreen>, IDisposable, IInitializable
    {
        private readonly Stack<IDisposable> _disposables = new();
        private readonly IPlainClassFactory _plainClassFactory;
        private readonly GameplayModel _gameplayModel;

        private MusicButtonHandler _musicButtonHandler;
        private SoundButtonHandler _soundButtonHandler;
        
        public GamePlayScreenMediator(IPlainClassFactory plainClassFactory, GameplayModel gameplayModel)
        {
            _plainClassFactory = plainClassFactory;
            _gameplayModel = gameplayModel;
        }

        public void Initialize()
        {
            _musicButtonHandler = _plainClassFactory.Create<MusicButtonHandler>();
            _musicButtonHandler.AddMusicButtonView(View.MusicButton);
            
            _soundButtonHandler = _plainClassFactory.Create<SoundButtonHandler>();
            _soundButtonHandler.AddSoundButtonView(View.SoundButton);
            
            _disposables.Push(_soundButtonHandler);
            _disposables.Push(_musicButtonHandler);
            
            
            View.WinButtonClicked += WinButtonClicked;
            View.LooseButtonClicked += LooseButtonClicked;
        }

        public void SetupLevelInfo(string levelInfo)
        {
            View.LevelTimeCounterView.SetupLevelInfo(levelInfo);
        }

        public void Dispose()
        {
            while (_disposables.Count > 0)
                _disposables.Pop().Dispose();
            
            View.WinButtonClicked -= WinButtonClicked;
            View.LooseButtonClicked -= LooseButtonClicked;
        }
        
        private void LooseButtonClicked()
        {
            _gameplayModel.LoseLevel();
        }

        private void WinButtonClicked()
        {
            _gameplayModel.WinLevel();
        }
    }
}