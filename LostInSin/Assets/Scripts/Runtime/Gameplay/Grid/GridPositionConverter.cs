using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public interface IGridPositionConverter
	{
		bool GetCell(Vector3 worldPosition, out GridCell cell);
		Vector3 GetWorldPoint(int row, int column);
		GridCell GetCell(int row, int column);
		Vector3 GetWorldPoint(GridCellData cellData);
		List<GridCell> GetCellsWithinRadius(GridCell centerCell, float radius);
	}

	public class GridPositionConverter : IGridPositionConverter
	{
		private readonly GridModel _gridModel;

		private GridPositionConverter(GridModel gridModel)
		{
			_gridModel = gridModel;
		}

		public bool GetCell(Vector3 worldPosition, out GridCell cell)
		{
			int row = Mathf.FloorToInt(worldPosition.x / _gridModel.gridCellWidth + _gridModel.gridRowOffset);
			int column = Mathf.FloorToInt(worldPosition.z / _gridModel.gridCellHeight + _gridModel.gridColumnOffset);

			bool positionIsInsideGrid = false;
			cell = null;

			// cell count is 1 less than row and column size
			if (row >= 0 && column >= 0 && row < _gridModel.gridRowCount - 1 && column < _gridModel.gridColumnCount - 1)
			{
				cell = _gridModel.GetGridCell(row, column);
				positionIsInsideGrid = true;
			}

			return positionIsInsideGrid;
		}

		public Vector3 GetWorldPoint(int row, int column)
		{
			GridCell gridCell = GetCell(row, column);
			return gridCell.centerPosition;
		}

		public GridCell GetCell(int row, int column)
		{
			return _gridModel.GetGridCell(row, column);
		}

		public Vector3 GetWorldPoint(GridCellData cellData)
		{
			return cellData.Center.ToVector3();
		}

		//returns a list of all GridCells within the specified radius from the center cell
		//using flood fill algorithm
		public List<GridCell> GetCellsWithinRadius(GridCell centerCell, float radius)
		{
			List<GridCell> result = new();
			HashSet<(int row, int col)> visited = new();
			Queue<(int row, int col)> queue = new();

			// start from the center cell.
			queue.Enqueue((centerCell.x, centerCell.y));
			visited.Add((centerCell.x, centerCell.y));

			Vector3 centerPos = centerCell.centerPosition;

			while (queue.Count > 0)
			{
				(int row, int col) current = queue.Dequeue();
				GridCell currentCell = _gridModel.GetGridCell(current.row, current.col);

				// check if currentCell is within radius
				if (Vector3.Distance(currentCell.centerPosition, centerPos) <= radius)
				{
					// add the cell to the results
					result.Add(currentCell);

					// flood-fill: enqueue neighbors
					foreach ((int row, int col) neighbor in GetNeighbors(current.row, current.col))
					{
						if (!visited.Contains(neighbor))
						{
							visited.Add(neighbor);
							queue.Enqueue(neighbor);
						}
					}
				}
			}

			return result;
		}

		// helper method to get the 4 orthogonal neighbors.
		private IEnumerable<(int row, int col)> GetNeighbors(int row, int col)
		{
			// 4-directional neighbors
			(int rowDirection, int columnDirection)[] directions =
			{
				(1, 0), // down
				(-1, 0), // up
				(0, 1), // right
				(0, -1) // left
			};

			foreach ((int rowDirection, int columnDirection) in directions)
			{
				int newRow = row + rowDirection;
				int newColumn = col + columnDirection;

				if (newRow >= 0 && newRow < _gridModel.gridRowCount && newColumn >= 0 && newColumn < _gridModel.gridColumnCount)
				{
					yield return (newRow, newColumn);
				}
			}
		}
	}
}