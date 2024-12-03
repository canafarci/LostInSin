using System;
using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
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
	public partial class MoveTowardsEnemyAction : Action
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> MoveAbility;

		private CharacterFacade _targetCharacter;

		protected override Status OnStart()
		{
			if (!TryFindTargetCharacter(out _targetCharacter))
				return Status.Failure;

			if (!TryFindBestMoveCell(_targetCharacter, out var closestCell, out var pathCells))
				return Status.Failure;

			if (!TryExecuteMove(closestCell, pathCells))
				return Status.Failure;

			return Status.Success;
		}

		private bool TryFindTargetCharacter(out CharacterFacade targetCharacter)
		{
			var playerCharacters = BTReferences.instance.charactersInSceneModel.playerCharactersInScene;

			if (playerCharacters == null)
			{
				targetCharacter = null;
				return false;
			}

			targetCharacter = playerCharacters
				.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - Agent.Value.transform.position))
				.FirstOrDefault();

			return targetCharacter != null;
		}

		private bool TryFindBestMoveCell(CharacterFacade targetCharacter, out GridCell closestCell,
			out List<GridCell> pathCells)
		{
			var npcCell = Agent.Value.currentCell;
			var targetCell = targetCharacter.currentCell;

			closestCell = null;
			pathCells = new List<GridCell>();
			float minCost = Mathf.Infinity;

			for (int x = targetCell.x - 1; x <= targetCell.x + 1; x++)
			{
				for (int y = targetCell.y - 1; y <= targetCell.y + 1; y++)
				{
					if (x == targetCell.x && y == targetCell.y)
						continue;

					var candidateCell = BTReferences.instance.gridPositionConverter.GetCell(x, y);

					if (candidateCell == null || candidateCell.isOccupied)
						continue;

					if (BTReferences.instance.gridPathfinder.FindPath(npcCell, candidateCell, out var path))
					{
						var pathCost = path.Count - 1;
						if (pathCost < minCost)
						{
							minCost = pathCost;
							closestCell = candidateCell;
							pathCells = path;
						}
					}
				}
			}

			return closestCell != null;
		}

		private bool TryExecuteMove(GridCell closestCell, List<GridCell> pathCells)
		{
			Ability ability = MoveAbility.Value;
			ability.AbilityRequest.Initialize(); //creates an ability request data inside Initialize func

			AbilityRequestData abilityRequestData = ability.AbilityRequest.data;
			abilityRequestData.TargetGridCell = closestCell;
			abilityRequestData.User = Agent.Value;
			abilityRequestData.PathCells = pathCells;


			if (!CanAffordAbility(ability))
				return false;


			Agent.Value.ReduceActionPoints(abilityRequestData.totalActionPointCost);
			ability.AbilityExecution.Initialize(abilityRequestData);

			BTReferences.instance.turnSystemFacade.AddAbilityForPlaying(ability.AbilityExecution);
			return true;
		}

		//this ability has dynamic cost, so check it during runtime
		private bool CanAffordAbility(Ability ability)
		{
			ability.AbilityRequest.UpdateRequest();
			return ability.AbilityRequest.data.totalActionPointCost <= Agent.Value.actionPoints;
		}
	}
}