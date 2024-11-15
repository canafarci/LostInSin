using System;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.BehaviourTree.Abilities
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "AnyAbilityIsNotBeingPlayed",
		story: "Any Ability Is Not Being Played [BTReferences]",
		category: "TurnConditions",
		id: "5a685ee432bdbd5ee6b1f4cd14146b31")]
	public partial class AnyAbilityIsNotBeingPlayedCondition : Condition
	{
		[SerializeReference] public BlackboardVariable<BTReferences> BTReferences;

		public override bool IsTrue()
		{
			return !BTReferences.Value.turnSystemFacade.isPlayingAbility;
		}
	}
}