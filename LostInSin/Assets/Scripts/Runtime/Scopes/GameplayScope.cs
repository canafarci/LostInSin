// GameplayScope.cs

using LostInSin.Runtime.Gameplay.GameplayLifecycle;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.GameStates;
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
		[SerializeField] private GridGenerationSo GridGenerationDataSo;
		[SerializeField] private GridVisualDataSo GridVisualDataSo;

		protected override void Configure(IContainerBuilder builder)
		{
			RegisterGameplayLifecycleManagers(builder);
			RegisterGridComponents(builder);
		}

		private void RegisterGameplayLifecycleManagers(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<GameplayEntryPoint>();
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
	}
}