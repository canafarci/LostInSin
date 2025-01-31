using System;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.CrossScene.LoadingScreen.Signals;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Visual;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry
{
	public class GameplayEntryPoint : IInitializable, IStartable, IDisposable
	{
		private readonly SignalBus _signalBus;
		private readonly ApplicationSettings _applicationSettings;
		private readonly GameplayInitializer _gameplayInitializer;


		public GameplayEntryPoint(SignalBus signalBus,
			ApplicationSettings applicationSettings,
			GameplayInitializer gameplayInitializer)
		{
			_signalBus = signalBus;
			_applicationSettings = applicationSettings;
			_gameplayInitializer = gameplayInitializer;
		}

		public void Initialize()
		{
			if (_applicationSettings.ShowLoadingScreen)
				_signalBus.Subscribe<LoadingFinishedSignal>(OnLoadingFinishedSignal);
		}

		private void OnLoadingFinishedSignal(LoadingFinishedSignal signal)
		{
			EnterGameplay();
		}

		public void Start()
		{
			if (!_applicationSettings.ShowLoadingScreen)
				EnterGameplay();
		}

		private async void EnterGameplay()
		{
			_signalBus.Fire(new ChangeAppStateSignal(AppStateID.Gameplay));
			_signalBus.Fire(new ChangeGameStateSignal(GameState.Initializing));

			await _gameplayInitializer.InitializeModules();

			_signalBus.Fire(new ChangeGameStateSignal(GameState.Playing));
		}


		public void Dispose()
		{
			if (_applicationSettings.ShowLoadingScreen)
				_signalBus.Unsubscribe<LoadingFinishedSignal>(OnLoadingFinishedSignal);
		}
	}
}