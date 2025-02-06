using LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals;
using LostInSin.Runtime.Infrastructure.Templates;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling
{
	public class PlayerAbilityRequestFillerInitializer : SignalListener
	{
		private readonly IPlayerAbilityRequestFillerModel _model;

		[Inject] private SelfTargetedHandler _selfTargetedHandler;
		[Inject] private EnemyTargetedHandler _enemyTargetedHandler;
		[Inject] private PositionRaycastedHandler _positionRaycastedHandler;
		[Inject] private GridPathFindingHandler _gridPathFindingHandler;
		[Inject] private GridPositionRaycastedHandler _gridPositionRaycastedHandler;
		[Inject] private EnemyTargetedPathfindingHandler _enemyTargetedPathfindingHandler;
		[Inject] private GridPositionRaycastedMovementHandler _gridPositionRaycastedMovementHandler;
		[Inject] private CircularAreaTargetedHandler _circularAreaTargetedHandler;

		public PlayerAbilityRequestFillerInitializer(IPlayerAbilityRequestFillerModel model)
		{
			_model = model;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}

		private void OnInitializeModulesSignalHandler(InitializeModulesSignal signal)
		{
			// Build chain in a logical order 
			// Each handler checks if it applies to the request's flags and, if so,
			// processes that logic. Because AbilityRequestType is [Flags], multiple
			// handlers might apply within the same request.


			// Chain them in the desired sequence for each player loop:
			_model.updateAbilityRequestTypeChain = _selfTargetedHandler;
			_selfTargetedHandler
				.SetNext(_enemyTargetedPathfindingHandler)
				.SetNext(_gridPathFindingHandler)
				.SetNext(_circularAreaTargetedHandler);

			_model.fixedUpdateAbilityRequestTypeChain = _enemyTargetedHandler;
			_enemyTargetedHandler
				.SetNext(_positionRaycastedHandler)
				.SetNext(_gridPositionRaycastedHandler)
				.SetNext(_gridPositionRaycastedMovementHandler);

			_signalBus.Fire(new ModuleInitializedSignal(InitializableModule.PlayerAbilityFiller));
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<InitializeModulesSignal>(OnInitializeModulesSignalHandler);
		}
	}
}