using System;
using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.BehaviourTree.AbilityConditions;
using LostInSin.Runtime.Gameplay.BehaviourTree.Templates;
using LostInSin.Runtime.Gameplay.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Abilities
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "UseMeleeAttackAbility",
	                 story: "[Agent] Uses [MeleeAttackAbility]",
	                 category: "Action",
	                 id: "49b31ec2e6b0fe633abeec194be26cdf")]
	public partial class UseMeleeAttackAbilityAction : AbilityAction
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> MeleeAttackAbility;

		protected override Status OnStart()
		{
			if (!GetClosestTargetCharacter(Agent.Value, out CharacterFacade targetCharacter))
				return Status.Failure;

			Ability ability = MeleeAttackAbility.Value;
			AbilityRequestData abilityRequestData = InitializeAbilityRequest(ability);


			if (AgentIsAtNeighboringCell(Agent.Value, targetCharacter))
			{
				PopulateAbilityRequestData(abilityRequestData, targetCharacter, new() { Agent.Value.currentCell }, Agent.Value.currentCell);
			}

			else if (TryFindBestMovementTargetCell(Agent.Value,
			                                       targetCharacter,
			                                       out GridCell closestCell,
			                                       out List<GridCell> pathCells))
			{
				PopulateAbilityRequestData(abilityRequestData, targetCharacter, pathCells, closestCell);
			}

			if (!CanAffordAbility(ability, Agent.Value))
				return Status.Failure;

			BTReferences.instance.turnSystemFacade.PlayAbility(ability);

			return Status.Success;
		}

		private void PopulateAbilityRequestData(AbilityRequestData abilityRequestData,
			CharacterFacade targetCharacter,
			List<GridCell> pathCells,
			GridCell closestCell)
		{
			abilityRequestData.User = Agent.Value;
			abilityRequestData.TargetCharacter = targetCharacter;
			abilityRequestData.PathCells = pathCells;
			abilityRequestData.TargetGridCell = closestCell;
		}
	}
}