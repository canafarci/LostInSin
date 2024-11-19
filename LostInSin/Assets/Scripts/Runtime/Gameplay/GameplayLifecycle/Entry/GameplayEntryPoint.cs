using System;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.CrossScene.LoadingScreen.Signals;
using LostInSin.Runtime.Gameplay.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.Visual;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry
{
	public class GameplayEntryPoint : IInitializable, IStartable, IDisposable
	{
		private readonly SignalBus _signalBus;
		private readonly ApplicationSettings _applicationSettings;
		private readonly GridGenerator _gridGenerator;
		private readonly GridMeshDisplayService _gridMeshDisplayService;
		private readonly CombatStartCharacterGridPositionSetter _combatStartCharacterGridPositionSetter;

		[Inject]
		public GameplayEntryPoint(
			SignalBus signalBus,
			ApplicationSettings applicationSettings,
			GridGenerator gridGenerator,
			GridMeshDisplayService gridMeshDisplayService,
			CombatStartCharacterGridPositionSetter combatStartCharacterGridPositionSetter)
		{
			_signalBus = signalBus;
			_applicationSettings = applicationSettings;
			_gridGenerator = gridGenerator;
			_gridMeshDisplayService = gridMeshDisplayService;
			_combatStartCharacterGridPositionSetter = combatStartCharacterGridPositionSetter;
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
		}

		private void InitializeGameplay()
		{
			// Generate the grid
			_gridGenerator.GenerateGrid();

			// Display the grid mesh
			_gridMeshDisplayService.ShowGrid();

			_combatStartCharacterGridPositionSetter.SetPositions();
		}

		public void Dispose()
		{
			if (_applicationSettings.ShowLoadingScreen)
				_signalBus.Unsubscribe<LoadingFinishedSignal>(OnLoadingFinishedSignal);
		}
	}
}