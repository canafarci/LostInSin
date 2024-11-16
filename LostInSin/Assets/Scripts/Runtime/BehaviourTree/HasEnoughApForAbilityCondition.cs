using System;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.BehaviourTree
{
	[Serializable, Unity.Properties.GeneratePropertyBag]
	[Condition(name: "HasEnoughAPForAbility",
		story: "[Agent] Has Enough AP For [Ability]",
		category: "TurnConditions",
		id: "13e866f820a8547e81a7f27b45613e91")]
	public partial class HasEnoughApForAbilityCondition : Condition
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> Ability;

		public override bool IsTrue()
		{
			return Agent.Value.actionPoints >= Ability.Value.DefaultActionPointCost;
		}
	}
}