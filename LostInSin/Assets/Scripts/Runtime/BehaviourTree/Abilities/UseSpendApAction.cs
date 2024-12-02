using System;
using LostInSin.Runtime.BehaviourTree.Utility;
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

			ability.AbilityRequest.Initialize();
			AbilityRequestData abilityRequestData = ability.AbilityRequest.data;
			abilityRequestData.User = Agent.Value;

			ability.AbilityExecution.Initialize(abilityRequestData);
			BTReferences.instance.turnSystemFacade.AddAbilityForPlaying(ability.AbilityExecution);

			Agent.Value.ReduceActionPoints(abilityRequestData.totalActionPointCost);

			return Status.Success;
		}
	}
}