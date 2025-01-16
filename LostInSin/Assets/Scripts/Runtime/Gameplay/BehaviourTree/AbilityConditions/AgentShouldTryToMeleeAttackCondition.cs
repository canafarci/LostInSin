using System;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.AbilityConditions
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "Agent Should Try To Melee Attack",
	           story: "[Agent] should try to Melee Attack",
	           category: "AbilityConditions",
	           id: "8f9d838a96fb8962285cc434a71580da")]
	public partial class AgentShouldTryToMeleeAttackCondition : Condition
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