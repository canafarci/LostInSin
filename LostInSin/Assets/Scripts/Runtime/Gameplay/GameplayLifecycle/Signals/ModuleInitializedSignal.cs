using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Enums;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals
{
	/// <summary>
	/// Fired from <see cref="PlayerAbilityRequestFillerInitializer"/> , <see cref="CombatGridInitializer"/> , <see cref="PlayerCharactersSpawner"/> and <see cref="TurnController"/>
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