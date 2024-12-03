using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Visual;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat
{
	public class TurnBasedCombatInitializer : SignalListener
	{
		private readonly GridGenerator _gridGenerator;
		private readonly GridMeshDisplayService _gridMeshDisplayService;
		private readonly CombatStartCharacterGridPositionSetter _combatStartCharacterGridPositionSetter;

		public TurnBasedCombatInitializer(GridGenerator gridGenerator,
			GridMeshDisplayService gridMeshDisplayService,
			CombatStartCharacterGridPositionSetter combatStartCharacterGridPositionSetter)
		{
			_gridGenerator = gridGenerator;
			_gridMeshDisplayService = gridMeshDisplayService;
			_combatStartCharacterGridPositionSetter = combatStartCharacterGridPositionSetter;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<InitializeTurnBasedCombatSignal>(OnInitializeTurnBasedCombatSignalHandler);
		}

		private void OnInitializeTurnBasedCombatSignalHandler(InitializeTurnBasedCombatSignal signal)
		{
			_gridGenerator.GenerateGrid();
			_gridMeshDisplayService.ShowGrid();

			_combatStartCharacterGridPositionSetter.SetPositions();

			_signalBus.Fire(new StartTurnBasedCombatSignal());
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<InitializeTurnBasedCombatSignal>(OnInitializeTurnBasedCombatSignalHandler);
		}
	}
}