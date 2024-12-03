using System;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.CrossScene.LoadingScreen.Signals;
using LostInSin.Runtime.Gameplay.Enums;
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

		[Inject]
		public GameplayEntryPoint(SignalBus signalBus,
			ApplicationSettings applicationSettings)
		{
			_signalBus = signalBus;
			_applicationSettings = applicationSettings;
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

			InitializeGameplay();

			await UniTask.Delay(TimeSpan.FromSeconds(1f));

			_signalBus.Fire(new ChangeGameStateSignal(GameState.Playing));

			_signalBus.Fire(new InitializeTurnBasedCombatSignal());
		}

		private void InitializeGameplay()
		{
		}

		public void Dispose()
		{
			if (_applicationSettings.ShowLoadingScreen)
				_signalBus.Unsubscribe<LoadingFinishedSignal>(OnLoadingFinishedSignal);
		}
	}
}