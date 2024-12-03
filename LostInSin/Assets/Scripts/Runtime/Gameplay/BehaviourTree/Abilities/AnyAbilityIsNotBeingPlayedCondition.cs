using System;
using LostInSin.Runtime.Gameplay.BehaviourTree.Utility;
using Unity.Behavior;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Abilities
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "AnyAbilityIsNotBeingPlayed",
	           story: "Any Ability Is Not Being Played",
	           category: "TurnConditions",
	           id: "5a685ee432bdbd5ee6b1f4cd14146b31")]
	public partial class AnyAbilityIsNotBeingPlayedCondition : Condition
	{
		public override bool IsTrue()
		{
			return !BTReferences.instance.turnSystemFacade.isPlayingAbility;
		}
	}
}