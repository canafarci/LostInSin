using System;
using System.Linq;
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
	[NodeDescription(name: "UseShootArrowAbility",
	                 story: "[Agent] Uses Shoot Arrow [Ability]",
	                 category: "Abilities",
	                 id: "1b762c9687d812baa1a5e90a3c7aa7d3")]
	public partial class UseShootArrowAbilityAction : AbilityAction
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> Ability;

		//This ability's Request data only requires a target character.
		//Set it by getting the closest target and then send the ability for execution
		protected override Status OnStart()
		{
			if (!GetClosestTargetCharacter(Agent.Value, out CharacterFacade targetCharacter))
				return Status.Failure;

			Ability ability = Ability.Value;

			AbilityRequestData abilityRequestData = InitializeAbilityRequest(ability);

			PopulateAbilityRequestData(abilityRequestData, targetCharacter);

			BTReferences.instance.turnSystemFacade.PlayAbility(ability);
			return Status.Success;
		}

		private void PopulateAbilityRequestData(AbilityRequestData abilityRequestData, CharacterFacade target)
		{
			abilityRequestData.User = Agent.Value;
			abilityRequestData.TargetCharacter = target;
		}
	}
}