using System;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	[Flags]
	public enum AbilityRequestType
	{
		SelfTargeted,
		PositionRaycasted,
		GridPositionRaycasted
	}
}