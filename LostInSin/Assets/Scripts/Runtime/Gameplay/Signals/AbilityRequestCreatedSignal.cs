using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;

namespace LostInSin.Runtime.Gameplay.Signals
{
	public readonly struct AbilityRequestCreatedSignal
	{
		public AbilityRequest request { get; }

		public AbilityRequestCreatedSignal(AbilityRequest abilityRequest)
		{
			request = abilityRequest;
		}
	}
}