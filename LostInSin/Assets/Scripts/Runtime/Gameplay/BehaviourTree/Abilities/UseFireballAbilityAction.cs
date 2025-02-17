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

namespace LostInSin.Runtime.Gameplay.BehaviourTree.Abilities
{
	[Serializable, GeneratePropertyBag]
	[NodeDescription(name: "UseFireballAbility",
	                 story: "[Agent] Casts [FireballAbility]",
	                 category: "Action",
	                 id: "e3c1a58a2d1f4f6fbd9e6f1b7a8c1234")]
	public partial class UseFireballAbilityAction : AbilityAction
	{
		[SerializeReference] public BlackboardVariable<CharacterFacade> Agent;
		[SerializeReference] public BlackboardVariable<Ability> FireballAbility;

		protected override Status OnStart()
		{
			Ability ability = FireballAbility.Value;
			AbilityRequestData abilityRequestData = InitializeAbilityRequest(ability);

			// Find the optimal target cell based on enemy positions
			if (!FindBestFireballTarget(Agent.Value, out GridCell bestTargetCell, out List<GridCell> affectedCells))
				return Status.Failure;

			// Populate the ability request data with the chosen target cell and affected area.
			PopulateRequestData(abilityRequestData, bestTargetCell, affectedCells);

			if (!CanAffordAbility(ability, Agent.Value))
				return Status.Failure;

			BTReferences.instance.turnSystemFacade.PlayAbility(ability);
			return Status.Success;
		}

		private bool FindBestFireballTarget(CharacterFacade agent, out GridCell bestTargetCell, out List<GridCell> bestAffectedCells)
		{
			bestTargetCell = null;
			bestAffectedCells = null;
			int maxHitCount = 0;

			// Retrieve all enemy characters (not on the same team and not dead)
			List<CharacterFacade> allCharacters = BTReferences.instance.charactersInSceneModel.allCharactersInScene;
			List<CharacterFacade> enemyCharacters = new List<CharacterFacade>();
			foreach (CharacterFacade character in allCharacters)
			{
				if (character.teamID != agent.teamID && !character.isDead)
					enemyCharacters.Add(character);
			}

			if (enemyCharacters.Count == 0)
				return false;

			// For each combination size from all enemies down to one enemy.
			for (int size = enemyCharacters.Count; size >= 1; size--)
			{
				foreach (List<CharacterFacade> combination in GetCombinations(enemyCharacters, size))
				{
					// Compute geometric center from the enemy cells in this combination.
					int sumX = 0, sumY = 0;
					foreach (CharacterFacade enemy in combination)
					{
						sumX += enemy.currentCell.x;
						sumY += enemy.currentCell.y;
					}

					int centerX = Mathf.RoundToInt(sumX / (float)combination.Count);
					int centerY = Mathf.RoundToInt(sumY / (float)combination.Count);

					GridCell candidateCell = BTReferences.instance.gridPositionConverter.GetCell(centerX, centerY);
					if (candidateCell == null)
						continue;

					// Determine the affected area using the ability's specified radius.
					List<GridCell> areaCells = BTReferences.instance.gridPositionConverter.GetCellsWithinRadius(
					                                                                                            candidateCell, FireballAbility.Value.AbilityRequest.Radius);

					// Count how many enemy characters would be hit.
					int hitCount = enemyCharacters.Count(enemy => areaCells.Contains(enemy.currentCell));

					if (hitCount > maxHitCount)
					{
						maxHitCount = hitCount;
						bestTargetCell = candidateCell;
						bestAffectedCells = areaCells;

						// Early exit if all enemies are hit.
						if (maxHitCount == enemyCharacters.Count)
							return true;
					}
				}
			}

			return bestTargetCell != null;
		}

		private IEnumerable<List<T>> GetCombinations<T>(List<T> list, int combinationSize)
		{
			if (combinationSize == 0)
			{
				yield return new List<T>();
				yield break;
			}

			if (list.Count < combinationSize)
				yield break;

			for (int i = 0; i <= list.Count - combinationSize; i++)
			{
				T head = list[i];
				List<T> remaining = list.GetRange(i + 1, list.Count - (i + 1));
				foreach (List<T> tailCombination in GetCombinations(remaining, combinationSize - 1))
				{
					List<T> combination = new List<T> { head };
					combination.AddRange(tailCombination);
					yield return combination;
				}
			}
		}

		private void PopulateRequestData(AbilityRequestData abilityRequestData, GridCell bestTargetCell, List<GridCell> affectedCells)
		{
			abilityRequestData.TargetGridCell = bestTargetCell;
			abilityRequestData.TargetGridCells = affectedCells;
			abilityRequestData.User = Agent.Value;
		}
	}
}