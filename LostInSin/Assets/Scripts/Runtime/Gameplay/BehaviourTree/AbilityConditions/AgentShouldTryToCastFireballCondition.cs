using System;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.AbilityConditions
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "AgentShouldTryToCastFireball",
	           story: "If [Agent] Should Cast Fireball",
	           category: "AbilityConditions",
	           id: "f0301fcce3fcd11f2cd30f33f1f07e3c")]
	public partial class AgentShouldTryToCastFireballCondition : Condition
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;

		public override bool IsTrue()
		{
			return true;
		}

		public override void OnStart()
		{
		}

		public override void OnEnd()
		{
		}
	}
}