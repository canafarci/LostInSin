using LostInSin.Runtime.Gameplay.GameplayLifecycle.Entry;
using LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;

namespace LostInSin.Runtime.Gameplay.Signals
{
	/// <summary>
	/// Signal used to Initialize turn based combat system. <br />
	/// Listened by  <see cref="TurnController"/>, <see cref="TurnBasedCombatInitializer"/> <br />
	/// Results in grid being drawn and turn order being calculated <br />
	/// Fired by <see cref="GameplayEntryPoint"/>
	/// </summary>
	public struct InitializeTurnBasedCombatSignal
	{
	}
}