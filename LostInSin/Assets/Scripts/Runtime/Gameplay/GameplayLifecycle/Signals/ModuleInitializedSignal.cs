using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals
{
	/// <summary>
	/// Fired from <see cref="TimerUIController"/> , <see cref="GoalObjectsController"/>  and <see cref="DockInitializer"/>> 
	/// Listened by <see cref="GameplayInitializer"/> <br/>
	/// </summary>
	public struct ModuleInitializedSignal
	{
		public InitializableModule initializableModule { get; }

		public ModuleInitializedSignal(InitializableModule initializableModule)
		{
			this.initializableModule = initializableModule;
		}
	}
}