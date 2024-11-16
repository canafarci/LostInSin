using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Grid.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace LostInSin.Runtime.Pathfinding
{
	public interface IGridPathfinder
	{
		bool FindPath(AbilityRequest abilityRequest, out List<GridCell> pathCells);
		bool FindPath(GridCell startCell, GridCell targetCell, out List<GridCell> pathCells);
	}
}