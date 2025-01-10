using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityPlaying;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals;
using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.GameplayLifecycle;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.GameStates;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.DataObjects;
using LostInSin.Runtime.Gameplay.Grid.Visual;
using LostInSin.Runtime.Gameplay.Pathfinding;
using LostInSin.Runtime.Gameplay.Raycast;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.UI.AbilityPanel;
using LostInSin.Runtime.Gameplay.UI.ActiveTurnCharacterInfoPanel;
using LostInSin.Runtime.Gameplay.UI.InitiativePanel;
using LostInSin.Runtime.Gameplay.UI.Turns;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Scopes
{
	public class GameplayScope : LifetimeScope
	{
		[SerializeField] private GridGenerationSO GridGenerationDataSo;
		[SerializeField] private GridVisualDataSO GridVisualDataSo;
		[SerializeField] private PlayerCharactersSO PlayerCharactersSO;
		[SerializeField] private List<CharacterFacade> EnemyCharactersInScene;
		[SerializeField] private List<AbilityView> AbilityViews;

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterInstances(builder);
			RegisterGameplayLifecycleManagers(builder);
			RegisterGridComponents(builder);
			RegisterServices(builder);
			RegisterTurnModule(builder);
			RegisterAbilityModule(builder);
			RegisterSignals(builder);
		}

		private void RegisterAbilityModule(IContainerBuilder builder)
		{
			RegisterAbilityUI(builder);

			builder.Register<PlayerAbilityRequestFiller>(Lifetime.Singleton).AsImplementedInterfaces();
			builder.Register<AbilityPlayer>(Lifetime.Singleton).AsImplementedInterfaces();
			builder.Register<PlayerRaycaster>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
			builder.Register<MoveAbilityVisualDisplayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
			builder.Register<MeleeAttackAbilityVisualDisplayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
		}

		private void RegisterAbilityUI(IContainerBuilder builder)
		{
			builder.Register<AbilityPanelMediator>(Lifetime.Singleton).AsImplementedInterfaces();
		}

		private void RegisterInstances(IContainerBuilder builder)
		{
			builder.RegisterInstance(EnemyCharactersInScene);
			builder.RegisterInstance(PlayerCharactersSO);
			builder.RegisterInstance(AbilityViews);
		}

		private void RegisterGameplayLifecycleManagers(IContainerBuilder builder)
		{
			//Entry
			builder.RegisterEntryPoint<GameplayEntryPoint>();
			builder.RegisterEntryPoint<PlayerCharactersSpawner>();

			builder.RegisterEntryPoint<GameplayExitPoint>();
			builder.RegisterEntryPoint<GameStateController>();
			builder.Register<IGameStateModel, GameStateModel>(Lifetime.Singleton);
		}

		private void RegisterGridComponents(IContainerBuilder builder)
		{
			// Register ScriptableObject instances
			builder.RegisterInstance(GridGenerationDataSo);
			builder.RegisterInstance(GridVisualDataSo);

			// Register GridModel and its dependencies
			builder.Register<GridModel>(Lifetime.Singleton)
				.WithParameter("data", new GridModel.Data
				{
					GridData = GridGenerationDataSo
				});

			// Register Grid Generators
			builder.Register<IGridPointsGenerator, GridPointsGenerator>(Lifetime.Singleton);
			builder.Register<IGridCellGenerator, GridCellGenerator>(Lifetime.Singleton);

			// Register GridRaycaster (assuming you have an implementation)
			builder.Register<IGridRaycaster, GridRaycaster>(Lifetime.Singleton);

			// Register GridGenerator
			builder.Register<GridGenerator>(Lifetime.Singleton);

			// Register GridMeshGenerator
			builder.Register<GridMeshGenerator>(Lifetime.Singleton);

			// Register GridMeshDisplayService
			builder.Register<GridMeshDisplayService>(Lifetime.Singleton)
				.WithParameter(GridVisualDataSo);

			// Register GridPositionConverter
			builder.Register<IGridPositionConverter, GridPositionConverter>(Lifetime.Singleton);
		}

		private void RegisterServices(IContainerBuilder builder)
		{
			builder.Register<ICharactersInSceneModel, CharactersInSceneModel>(Lifetime.Singleton);
			builder.Register<GridPathfinder>(Lifetime.Singleton).AsImplementedInterfaces();

			builder.RegisterEntryPoint<CharactersInSceneController>();
			builder.RegisterEntryPoint<CombatStartCharacterGridPositionSetter>().AsSelf();

			builder.Register<TurnSystemFacade>(Lifetime.Singleton).AsSelf();
		}

		private void RegisterTurnModule(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<TurnController>();
			builder.RegisterEntryPoint<TurnMediator>().AsSelf();
			builder.RegisterComponentInHierarchy<TurnView>().AsSelf();
			builder.Register<ITurnModel, TurnModel>(Lifetime.Singleton);

			builder.RegisterComponentInHierarchy<ActiveTurnCharacterInfoPanelView>().AsSelf();
			builder.RegisterEntryPoint<ActiveTurnCharacterInfoPanelController>();

			builder.RegisterEntryPoint<InitiativePanelController>();
			builder.RegisterComponentInHierarchy<InitiativePanelView>().AsSelf();

			builder.RegisterEntryPoint<TurnBasedCombatInitializer>();
		}

		private void RegisterSignals(IContainerBuilder builder)
		{
			builder.DeclareSignal<GameStateChangedSignal>();
			builder.DeclareSignal<ChangeGameStateSignal>();
			builder.DeclareSignal<TriggerLevelEndSignal>();
			builder.DeclareSignal<ExitGameplayLevelSignal>();
			builder.DeclareSignal<CharacterAPChangedSignal>();
			builder.DeclareSignal<ActiveTurnCharacterChangedSignal>();
			builder.DeclareSignal<EndCharacterTurnSignal>();
			builder.DeclareSignal<AbilityRequestCreatedSignal>();
			builder.DeclareSignal<AbilityRequestCancelledSignal>();
			builder.DeclareSignal<AnimationEventSignal>();
			builder.DeclareSignal<InitializeTurnBasedCombatSignal>();
			builder.DeclareSignal<StartTurnBasedCombatSignal>();
		}
	}
}