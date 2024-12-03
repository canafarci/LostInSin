using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid.Data;

namespace LostInSin.Runtime.Gameplay.Pathfinding
{
	public interface IGridPathfinder
	{
		bool FindPath(AbilityRequest abilityRequest, out List<GridCell> pathCells);
		bool FindPath(GridCell startCell, GridCell targetCell, out List<GridCell> pathCells);
	}
}