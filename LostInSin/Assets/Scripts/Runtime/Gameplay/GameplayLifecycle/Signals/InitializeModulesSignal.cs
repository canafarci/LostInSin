using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;

namespace LostInSin.Runtime.Gameplay.GameplayLifecycle.Signals
{
	/// <summary>
	/// Fired from <see cref="GameplayInitializer"/> <br/>
	/// Listened by <see cref="PlayerAbilityRequestFillerInitializer"/> , <see cref="CombatGridInitializer"/> , <see cref="PlayerCharactersSpawner"/> and <see cref="TurnController"/>
	/// </summary>
	public readonly struct InitializeModulesSignal
	{
	}
}