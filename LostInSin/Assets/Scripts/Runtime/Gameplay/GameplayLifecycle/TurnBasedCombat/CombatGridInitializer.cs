using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Visual;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat
{
	public class CombatGridInitializer : SignalListener
	{
		private readonly GridGenerator _gridGenerator;
		private readonly GridMeshDisplayService _gridMeshDisplayService;
		private readonly CombatStartCharacterGridPositionSetter _combatStartCharacterGridPositionSetter;

		public CombatGridInitializer(GridGenerator gridGenerator,
			GridMeshDisplayService gridMeshDisplayService,
			CombatStartCharacterGridPositionSetter combatStartCharacterGridPositionSetter)
		{
			_gridGenerator = gridGenerator;
			_gridMeshDisplayService = gridMeshDisplayService;
			_combatStartCharacterGridPositionSetter = combatStartCharacterGridPositionSetter;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}

		private void OnInitializeModulesSignalHandler(InitializeModulesSignal signal)
		{
			_gridGenerator.GenerateGrid();
			_gridMeshDisplayService.ShowGrid();

			_combatStartCharacterGridPositionSetter.SetPositions();

			_signalBus.Fire(new ModuleInitializedSignal(InitializableModule.Grid));
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}
	}
}