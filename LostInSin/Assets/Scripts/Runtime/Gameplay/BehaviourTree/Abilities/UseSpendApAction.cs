using System;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.BehaviourTree.Templates;
using LostInSin.Runtime.Gameplay.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Characters;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Abilities
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "UseSpendAPAction",
	                 story: "[Agent] uses [SpendAPAbility]",
	                 category: "Action",
	                 id: "9a47957bb200851e8de6949f1356d809")]
	public partial class UseSpendApAction : AbilityAction
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> SpendAPAbility;

		protected override Status OnStart()
		{
			Ability ability = SpendAPAbility.Value;
			AbilityRequestData abilityRequestData = InitializeAbilityRequest(ability);

			abilityRequestData.User = Agent.Value;

			BTReferences.instance.turnSystemFacade.PlayAbility(ability);

			return Status.Success;
		}
	}
}