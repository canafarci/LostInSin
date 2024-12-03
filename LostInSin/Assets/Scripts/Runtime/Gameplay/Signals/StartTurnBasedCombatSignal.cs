using LostInSin.Runtime.Gameplay.GameplayLifecycle.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.TurnBasedCombat;
using LostInSin.Runtime.Gameplay.UI.InitiativePanel;

namespace LostInSin.Runtime.Gameplay.Signals
{
	/// <summary>
	/// Signal used to Start turn based combat system. <br />
	/// Listened by  <see cref="TurnController"/>, <see cref="InitiativePanelController"/> <br />
	/// Results a character taking control and initiative panel being drawn <br />
	/// Fired by <see cref="TurnBasedCombatInitializer"/>
	/// </summary>
	public struct StartTurnBasedCombatSignal
	{
	}
}