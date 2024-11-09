using LostInSin.Runtime.Gameplay.GameplayLifecycle;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.GameStates;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Scopes
{
	public class GameplayScope : LifetimeScope
	{
		protected override void Configure(IContainerBuilder builder)
		{
			RegisterGameplayLifecycleManagers(builder);
		}

		private void RegisterGameplayLifecycleManagers(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<GameplayEntryPoint>();
			builder.RegisterEntryPoint<GameplayExitPoint>();
			builder.RegisterEntryPoint<GameStateController>();
			builder.Register<IGameStateModel, GameStateModel>(Lifetime.Singleton);
		}
	}
}