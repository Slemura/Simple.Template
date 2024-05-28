using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace RpDev.Services.UI
{
	public class UIService : IUIService, IStartable
	{
		private readonly UIScreenFactory _uiScreenFactory;
		private readonly UIServicePresenter _presenter;
		
		private readonly Dictionary<Type, UIScreen> _shownScreens = new Dictionary<Type, UIScreen>();

		private bool _isPrototypeLoaded;

		public UIService(UIScreenFactory uiScreenFactory, UIServicePresenter presenter)
		{
			_uiScreenFactory = uiScreenFactory;
			_presenter = presenter;
		}
		
		public void Start()
		{
			_isPrototypeLoaded = false;
			LoadPrototypes().Forget();
		}

		private async UniTaskVoid LoadPrototypes()
		{
			await _uiScreenFactory.LoadPrototypes();
			_isPrototypeLoaded = true;
		}

		public async UniTask<TScreen> SpawnScreen<TScreen> () where TScreen : UIScreen
		{
			if(_isPrototypeLoaded == false)
				await UniTask.WaitUntil(() => _isPrototypeLoaded);
			
			if (_shownScreens.TryGetValue(typeof(TScreen), out var screen))
				return (TScreen)screen;

			screen = _uiScreenFactory.CreateScreen<TScreen>(_presenter.ScreenRoot);
			
			_shownScreens.Add(typeof(TScreen), screen);

			return (TScreen)screen;
		}

		public void DestroyScreen<TScreen> (TScreen screen) where TScreen : UIScreen
		{
			if (_shownScreens.TryGetValue(typeof(TScreen), out var shownScreen) == false)
				throw new Exception($"UI Screen '{typeof(TScreen).Name}' is not shown.");

			if (shownScreen != screen)
				throw new Exception($"UI Screen '{typeof(TScreen).Name}' is not the same as shown screen.");

			_shownScreens.Remove(typeof(TScreen));

			Object.Destroy(screen.gameObject);
		}
	}
}
