using LostInSin.Runtime.Gameplay.Abilities.RequestFilling;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Enums;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Pathfinding;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Visuals
{
	public class MoveMovingAbilityVisualDisplayer : MovingAbilityVisualDisplayer
	{
		public MoveMovingAbilityVisualDisplayer(IGridPathfinder gridPathfinder, PlayerRaycaster playerRaycaster)
			: base(gridPathfinder, playerRaycaster)
		{
		}

		protected override bool IsRequestRelevant(AbilityRequest request)
		{
			// Only consider if user is the player, request uses GridPathFinding and is NOT EnemyTargeted
			if (request.data.User.teamID != TeamID.Player) return false;
			if (!request.RequestType.HasFlag(AbilityRequestType.GridPathFinding)) return false;
			if (request.RequestType.HasFlag(AbilityRequestType.EnemyTargeted)) return false;
			return true;
		}

		protected override int CalculateActionPointCost(System.Collections.Generic.List<GridCell> path, CharacterFacade user)
		{
			// For moves:
			// By default: cost = request's default AP cost + path.Count - 1 
			// (The path cost is  one less than path length.)
			int baseCost = _abilityRequest.data.DefaultActionPointCost;
			return baseCost + (path.Count - 1);
		}
	}
}