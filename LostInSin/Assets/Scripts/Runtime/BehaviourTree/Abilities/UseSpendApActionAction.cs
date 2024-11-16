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
		story: "[Agent] uses [SpendAPAbility] [BTReference]",
		category: "Action",
		id: "9a47957bb200851e8de6949f1356d809")]
	public partial class UseSpendApActionAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> SpendAPAbility;
		[SerializeReference] public BlackboardVariable<BTReferences> BTReference;

		protected override Status OnStart()
		{
			Ability ability = SpendAPAbility.Value;

			ability.AbilityRequest.Initialize(new AbilityRequestData());
			ability.AbilityRequest.StartRequest();
			ability.AbilityRequest.data.User = Agent.Value;

			ability.AbilityExecutionLogic.Initialize(ability.AbilityRequest.data);
			BTReference.Value.turnSystemFacade.AddAbilityForPlaying(ability.AbilityExecutionLogic);

			Agent.Value.ReduceActionPoints(ability.ActionPointCost);

			return Status.Success;
		}
	}
}