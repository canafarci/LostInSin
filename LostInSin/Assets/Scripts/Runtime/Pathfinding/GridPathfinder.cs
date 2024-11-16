using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Grid;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Pathfinding;
using UnityEngine;
using VContainer.Unity;

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
				GridCellData cellData = _gridModel.gridCells[row, col];
				if (cellData.IsInvalid)
					continue; // Skip invalid cells

				GridCell cell = _gridModel.GetGridCellData(row, col);
				_cellPositions[cell] = (row, col);
			}
		}
	}

	public bool FindPath(GridCell startCell, GridCell targetCell, out List<GridCell> pathCells)
	{
		pathCells = new List<GridCell>();
		return FindPath(ref pathCells, startCell, targetCell);
	}

	public bool FindPath(AbilityRequest abilityRequest, out List<GridCell> pathCells)
	{
		pathCells = new List<GridCell>();
		if (abilityRequest.data.TargetGridCell == null) return false;
		if (abilityRequest.data.PathCells != null) return false; //path has been already set

		GridCell startCell = abilityRequest.data.User.currentCell;
		GridCell targetCell = abilityRequest.data.TargetGridCell;

		return FindPath(ref pathCells, startCell, targetCell);
	}

	private bool FindPath(ref List<GridCell> pathCells, GridCell startCell, GridCell targetCell)
	{
		InitializePathfinder();

		// Check if startCell and targetCell are valid
		if (!_cellPositions.ContainsKey(startCell) || !_cellPositions.ContainsKey(targetCell))
			return false; // Start or target cell is invalid

		var openSet = new HashSet<GridCell> { startCell };
		var closedSet = new HashSet<GridCell>();

		var cameFrom = new Dictionary<GridCell, GridCell>();
		var gScore = new Dictionary<GridCell, float> { [startCell] = 0 };
		var fScore = new Dictionary<GridCell, float> { [startCell] = HeuristicCostEstimate(startCell, targetCell) };

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

			foreach (var neighbor in GetNeighbors(current))
			{
				if (closedSet.Contains(neighbor) || neighbor.isOccupied)
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
		var neighbors = new List<GridCell>();
		var (row, col) = GetCellPosition(cell);

		var directions = new (int dRow, int dCol)[]
		{
			(-1, 0), // Up
			(1, 0), // Down
			(0, -1), // Left
			(0, 1) // Right
		};

		foreach (var (dRow, dCol) in directions)
		{
			int newRow = row + dRow;
			int newCol = col + dCol;

			if (newRow >= 0 && newRow < _gridModel.gridRowCount && newCol >= 0 && newCol < _gridModel.gridColumnCount)
			{
				GridCellData neighborCellData = _gridModel.gridCells[newRow, newCol];
				if (neighborCellData.IsInvalid)
					continue; // Skip invalid cells

				GridCell neighborCell = _gridModel.GetGridCellData(newRow, newCol);
				if (!_cellPositions.ContainsKey(neighborCell))
					continue; // Neighbor is invalid or not accessible

				neighbors.Add(neighborCell);
			}
		}

		return neighbors;
	}

	private (int row, int column) GetCellPosition(GridCell cell)
	{
		if (_cellPositions.TryGetValue(cell, out var position))
			return position;

		throw new Exception("Cell position not found.");
	}

	private List<GridCell> ReconstructPath(Dictionary<GridCell, GridCell> cameFrom, GridCell current)
	{
		var path = new List<GridCell> { current };
		while (cameFrom.TryGetValue(current, out current))
		{
			path.Insert(0, current);
		}

		return path;
	}
}