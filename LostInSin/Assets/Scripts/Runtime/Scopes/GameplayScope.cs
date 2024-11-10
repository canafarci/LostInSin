// GameplayScope.cs

using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Data;
using LostInSin.Runtime.Gameplay.Data.SceneReferences;
using LostInSin.Runtime.Gameplay.GameplayLifecycle;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.GameStates;
using LostInSin.Runtime.Gameplay.Turns;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.DataObjects;
using LostInSin.Runtime.Grid.Visual;
using LostInSin.Runtime.Raycast;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Scopes
{
	public class GameplayScope : LifetimeScope
	{
		[SerializeField] private GridGenerationSO GridGenerationDataSo;
		[SerializeField] private GridVisualDataSO GridVisualDataSo;
		[SerializeField] private List<CharacterFacade> EnemyCharactersInScene;
		[SerializeField] private PlayerCharactersSO PlayerCharactersSO;

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterInstances(builder);
			RegisterGameplayLifecycleManagers(builder);
			RegisterGridComponents(builder);
			RegisterServices(builder);

			builder.RegisterEntryPoint<TurnController>();
		}

		private void RegisterInstances(IContainerBuilder builder)
		{
			builder.RegisterInstance(EnemyCharactersInScene);
			builder.RegisterInstance(PlayerCharactersSO);
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
			builder.RegisterEntryPoint<CharacterController>();
		}
	}
}