using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Infrastructure.MemoryPool;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution
{
	public class AbilityExecutionData : Poolable
	{
		public HashSet<StringAsset> AbilityTriggers = new();

		public override void OnGetFromPool()
		{
			AbilityTriggers.Clear();
		}
	}
}