using System;
using System.Collections.Generic;
using System.Linq;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Pathfinding
{
	public interface IGridPathfinder
	{
		bool FindPath(GridCell startCell, GridCell targetCell, out List<GridCell> pathCells);
	}

	public class GridPathfinder : IGridPathfinder
	{
		private readonly GridModel _gridModel;

		private readonly Dictionary<GridCell, (int row, int column)> _cellPositions = new();

		public GridPathfinder(GridModel gridModel)
		{
			_gridModel = gridModel;
		}

		private void InitializePathfinder()
		{
			// Build a mapping from GridCell to its position and store GridCellData
			for (int row = 0; row < _gridModel.gridRowCount - 1; row++)
			{
				for (int col = 0; col < _gridModel.gridColumnCount - 1; col++)
				{
					GridCellData cellData = _gridModel.gridCellsData[row, col];
					if (cellData.IsInvalid)
						continue; // Skip invalid cells

					GridCell cell = _gridModel.GetGridCell(row, col);
					_cellPositions[cell] = (row, col);
				}
			}
		}

		public bool FindPath(GridCell startCell, GridCell targetCell, out List<GridCell> pathCells)
		{
			pathCells = new();
			return FindPath(ref pathCells, startCell, targetCell);
		}

		private bool FindPath(ref List<GridCell> pathCells, GridCell startCell, GridCell targetCell)
		{
			InitializePathfinder();

			// Check if startCell and targetCell are valid
			if (!_cellPositions.ContainsKey(startCell) || !_cellPositions.ContainsKey(targetCell))
				return false; // Start or target cell is invalid

			HashSet<GridCell> openSet = new() { startCell };
			HashSet<GridCell> closedSet = new();

			Dictionary<GridCell, GridCell> cameFrom = new();
			Dictionary<GridCell, float> gScore = new() { [startCell] = 0 };
			Dictionary<GridCell, float> fScore = new() { [startCell] = HeuristicCostEstimate(startCell, targetCell) };

			while (openSet.Count > 0)
			{
				GridCell current = openSet.OrderBy(cell => fScore.ContainsKey(cell) ? fScore[cell] : float.PositiveInfinity)
					.First();

				if (current == targetCell)
				{
					pathCells = ReconstructPath(cameFrom, current);
					return true;
				}

				openSet.Remove(current);
				closedSet.Add(current);

				foreach (GridCell neighbor in GetNeighbors(current))
				{
					if (closedSet.Contains(neighbor) ||
					    (neighbor != targetCell && neighbor.isOccupied))
						continue;

					float tentativeGScore = gScore[current] + DistanceBetween(current, neighbor);

					if (!openSet.Contains(neighbor))
						openSet.Add(neighbor);
					else if (tentativeGScore >= (gScore.ContainsKey(neighbor) ? gScore[neighbor] : float.PositiveInfinity))
						continue;

					cameFrom[neighbor] = current;
					gScore[neighbor] = tentativeGScore;
					fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, targetCell);
				}
			}

			return false; // Path not found
		}


		private float HeuristicCostEstimate(GridCell a, GridCell b)
		{
			Vector3 posA = a.centerPosition;
			Vector3 posB = b.centerPosition;

			return Vector3.Distance(posA, posB); // Euclidean distance
		}

		private float DistanceBetween(GridCell a, GridCell b)
		{
			Vector3 posA = a.centerPosition;
			Vector3 posB = b.centerPosition;

			return Vector3.Distance(posA, posB);
		}

		private IEnumerable<GridCell> GetNeighbors(GridCell cell)
		{
			List<GridCell> neighbors = new();
			(int row, int col) = GetCellPosition(cell);

			(int dRow, int dCol)[] directions = new (int dRow, int dCol)[]
			{
				(-1, 0), // Up
				(1, 0), // Down
				(0, -1), // Left
				(0, 1) // Right
			};

			foreach ((int dRow, int dCol) in directions)
			{
				int newRow = row + dRow;
				int newCol = col + dCol;

				if (newRow >= 0 && newRow < _gridModel.gridRowCount && newCol >= 0 && newCol < _gridModel.gridColumnCount)
				{
					GridCellData neighborCellData = _gridModel.gridCellsData[newRow, newCol];
					if (neighborCellData.IsInvalid)
						continue; // Skip invalid cells

					GridCell neighborCell = _gridModel.GetGridCell(newRow, newCol);
					if (!_cellPositions.ContainsKey(neighborCell))
						continue; // Neighbor is invalid or not accessible

					neighbors.Add(neighborCell);
				}
			}

			return neighbors;
		}

		private (int row, int column) GetCellPosition(GridCell cell)
		{
			if (_cellPositions.TryGetValue(cell, out (int row, int column) position))
				return position;

			throw new("Cell position not found.");
		}

		private List<GridCell> ReconstructPath(Dictionary<GridCell, GridCell> cameFrom, GridCell current)
		{
			List<GridCell> path = new() { current };
			while (cameFrom.TryGetValue(current, out current))
			{
				path.Insert(0, current);
			}

			return path;
		}
	}
}