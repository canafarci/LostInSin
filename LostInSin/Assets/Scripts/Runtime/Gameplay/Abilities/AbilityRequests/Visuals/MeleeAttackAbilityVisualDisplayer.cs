using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Pathfinding;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals
{
	public class MeleeAttackAbilityVisualDisplayer : AbilityVisualDisplayer
	{
		public MeleeAttackAbilityVisualDisplayer(IGridPathfinder gridPathfinder, PlayerRaycaster playerRaycaster)
			: base(gridPathfinder, playerRaycaster)
		{
		}

		protected override bool IsRequestRelevant(AbilityRequest request)
		{
			// Only consider if user is the player, request uses GridPathFinding and is EnemyTargeted
			if (!request.data.User.isPlayerCharacter) return false;
			if (!request.RequestType.HasFlag(AbilityRequestType.GridPathFinding)) return false;
			if (!request.RequestType.HasFlag(AbilityRequestType.EnemyTargeted)) return false;
			return true;
		}

		protected override int CalculateActionPointCost(System.Collections.Generic.List<GridCell> path, CharacterFacade user)
		{
			// For melee attacks: 
			// By default: cost = request's default AP cost + path length - 2 
			// (Because the last cell isn't moved onto if the user is attacking from adjacency.)
			int baseCost = _abilityRequest.data.DefaultActionPointCost;
			return baseCost + (path.Count - 2);
		}

		/// <summary>
		/// Clear the line renderer when raycasting the grid, otherwise there are two visible
		/// lines at the same time
		/// </summary>
		protected override void OnFixedTickDisplay()
		{
			// For melee attacks, we also allow targeting actual characters:
			if (_playerRaycaster.TryRaycastForComponent(out CharacterFacade character))
			{
				DisplayPathAsLine(character.currentCell);
			}
			else if (_playerRaycaster.TryRaycastForGridCell(out GridCell targetCell))
			{
				ClearLineRenderer();
				DisplayPathAsLine(targetCell);
			}
		}
	}
}