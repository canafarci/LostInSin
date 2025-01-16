using System;
using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
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
	[NodeDescription(name: "MoveTowardsEnemy",
	                 story: "[Agent] uses [MoveAbility] Towards Target",
	                 category: "Action",
	                 id: "eb6866cb9e72c98cd920d3e7565218e3")]
	public partial class MoveTowardsEnemyAction : AbilityAction
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> MoveAbility;

		private CharacterFacade _targetCharacter;

		protected override Status OnStart()
		{
			if (!GetClosestTargetCharacter(Agent.Value, out _targetCharacter))
				return Status.Failure;

			if (!TryFindBestMovementTargetCell(Agent.Value, _targetCharacter, out GridCell closestCell, out List<GridCell> pathCells))
				return Status.Failure;

			if (!TryExecuteMove(closestCell, pathCells))
				return Status.Failure;

			return Status.Success;
		}


		private bool TryExecuteMove(GridCell closestCell, List<GridCell> pathCells)
		{
			Ability ability = MoveAbility.Value;

			AbilityRequestData abilityRequestData = InitializeAbilityRequest(ability);
			PopulateAbilityRequestData(closestCell, pathCells, abilityRequestData);

			if (!CanAffordAbility(ability, Agent.Value))
				UpdatePath(ability, abilityRequestData);

			BTReferences.instance.turnSystemFacade.PlayAbility(ability);
			return true;
		}

		private void PopulateAbilityRequestData(GridCell closestCell, List<GridCell> pathCells, AbilityRequestData abilityRequestData)
		{
			abilityRequestData.TargetGridCell = closestCell;
			abilityRequestData.User = Agent.Value;
			abilityRequestData.PathCells = pathCells;
		}

		private void UpdatePath(Ability ability, AbilityRequestData abilityRequestData)
		{
			int actionPoints = Agent.Value.actionPoints;
			int actionCost = ability.AbilityRequest.data.totalActionPointCost;
			int actionPointDelta = Mathf.Abs(actionPoints - actionCost);

			while (actionPointDelta > 0)
			{
				actionPointDelta--;
				abilityRequestData.PathCells.RemoveAt(abilityRequestData.PathCells.Count - 1);
			}
		}
	}
}