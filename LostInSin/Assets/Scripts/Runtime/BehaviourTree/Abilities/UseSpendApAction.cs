using System;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.BehaviourTree.Abilities
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "UseSpendAPAction",
		story: "[Agent] uses [SpendAPAbility]",
		category: "Action",
		id: "9a47957bb200851e8de6949f1356d809")]
	public partial class UseSpendApAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> SpendAPAbility;

		protected override Status OnStart()
		{
			Ability ability = SpendAPAbility.Value;

			AbilityRequestData abilityRequestData = new AbilityRequestData(ability.DefaultActionPointCost);
			ability.AbilityRequest.Initialize(abilityRequestData);
			ability.AbilityRequest.StartRequest();
			ability.AbilityRequest.data.User = Agent.Value;

			ability.AbilityExecutionLogic.Initialize(ability.AbilityRequest.data);
			BTReferences.instance.turnSystemFacade.AddAbilityForPlaying(ability.AbilityExecutionLogic);

			Agent.Value.ReduceActionPoints(ability.DefaultActionPointCost);

			return Status.Success;
		}
	}
}