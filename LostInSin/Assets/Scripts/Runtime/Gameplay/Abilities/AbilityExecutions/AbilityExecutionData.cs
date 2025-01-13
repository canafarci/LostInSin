using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Infrastructure.MemoryPool;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions
{
	public class AbilityExecutionData : Poolable
	{
		public HashSet<StringAsset> AbilityTriggers = new();
		public CharacterFacade User;


		public override void OnGetFromPool()
		{
			AbilityTriggers = new();
			User = null;
		}
	}
}