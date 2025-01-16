using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.BehaviourTree.Utility;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using Unity.Behavior;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Templates
{
	public abstract class AbilityAction : Action
	{
		protected static AbilityRequestData InitializeAbilityRequest(Ability ability)
		{
			ability.AbilityRequest.Initialize(); //creates an ability request data inside Initialize func
			AbilityRequestData abilityRequestData = ability.AbilityRequest.data;
			return abilityRequestData;
		}

		protected static bool GetClosestTargetCharacter(CharacterFacade agent, out CharacterFacade targetCharacter)
		{
			List<CharacterFacade> playerCharacters = BTReferences.instance.charactersInSceneModel.playerCharactersInScene;

			if (playerCharacters == null)
			{
				targetCharacter = null;
				return false;
			}

			targetCharacter = playerCharacters
				.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - agent.transform.position))
				.FirstOrDefault(x => x.isDead == false);

			return targetCharacter != null;
		}

		protected static bool AgentIsAtNeighboringCell(CharacterFacade agent,
			CharacterFacade targetCharacter)
		{
			GridCell targetCell = targetCharacter.currentCell;
			GridCell npcCell = agent.currentCell;

			for (int x = targetCell.x - 1; x <= targetCell.x + 1; x++)
			{
				for (int y = targetCell.y - 1; y <= targetCell.y + 1; y++)
				{
					if (x == targetCell.x && y == targetCell.y)
						continue;

					//character already occupies one of cells neighboring the target
					if (x == npcCell.x && y == npcCell.y)
					{
						return true;
					}
				}
			}

			return false;
		}

		protected static bool TryFindBestMovementTargetCell(CharacterFacade agent,
			CharacterFacade targetCharacter,
			out GridCell closestCell,
			out List<GridCell> pathCells)
		{
			closestCell = null;
			pathCells = new List<GridCell>();

			if (AgentIsAtNeighboringCell(agent, targetCharacter))
				return false;

			GridCell npcCell = agent.currentCell;
			GridCell targetCell = targetCharacter.currentCell;

			float minCost = Mathf.Infinity;

			for (int x = targetCell.x - 1; x <= targetCell.x + 1; x++)
			{
				for (int y = targetCell.y - 1; y <= targetCell.y + 1; y++)
				{
					if (x == targetCell.x && y == targetCell.y)
						continue;

					GridCell candidateCell = BTReferences.instance.gridPositionConverter.GetCell(x, y);

					if (candidateCell == null || candidateCell.isOccupied)
						continue;

					if (BTReferences.instance.gridPathfinder.FindPath(npcCell, candidateCell, out List<GridCell> path))
					{
						int pathCost = path.Count - 1;
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

		protected static bool CanAffordAbility(Ability ability, CharacterFacade agent)
		{
			ability.AbilityRequest.UpdateRequest();
			return ability.AbilityRequest.data.totalActionPointCost <= agent.actionPoints;
		}
	}
}