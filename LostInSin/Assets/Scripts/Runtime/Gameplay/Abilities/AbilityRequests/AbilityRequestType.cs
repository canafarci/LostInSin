using System;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	[Flags]
	public enum AbilityRequestType
	{
		SelfTargeted = 1 << 1,
		PositionRaycasted = 1 << 2,
		GridPositionRaycasted = 1 << 3,
		GridPathFinding = 1 << 4,
		Movement = 1 << 5,
		EnemyTargeted = 1 << 6,
	}
}