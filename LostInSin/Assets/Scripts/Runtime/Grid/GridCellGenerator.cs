using LostInSin.Runtime.Grid.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public class GridCellGenerator : IGridCellGenerator
	{
		private const float RAY_ORIGIN_HEIGHT = 10f;
		private readonly GridModel _gridModel;
		private readonly int _groundLayerMask;

		public GridCellGenerator(GridModel gridModel)
		{
			_groundLayerMask = LayerMask.GetMask("Ground");
			_gridModel = gridModel;
		}

		public (GridCell[,] cells, GridCellData[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints)
		{
			using (gridPoints)
			{
				var numCellsRow = _gridModel.gridRowCount;
				var numCellsColumn = _gridModel.gridColumnCount;

				GridCell[,] gridCells = new GridCell[numCellsRow, numCellsColumn];
				GridCellData[,] gridCellsData = new GridCellData[numCellsRow, numCellsColumn];

				for (var x = 0; x < numCellsRow; x++)
				for (var y = 0; y < numCellsColumn; y++)
					ProcessCell(gridPoints, gridCells, gridCellsData, x, y);

				return (gridCells, gridCellsData);
			}
		}

		private void ProcessCell(NativeArray<GridPoint> gridPoints,
			GridCell[,] gridCells,
			GridCellData[,] gridCellsData,
			int x,
			int y)
		{
			var topLeftIndex =
				x +
				y *
				(_gridModel.gridRowCount +
				 1); // add one to side length value, as grid cells count is  1 less than each side count
			var topRightIndex = topLeftIndex + 1;
			var bottomLeftIndex =
				topLeftIndex +
				_gridModel.gridRowCount +
				1; // add one to side length value, as grid cells count is  1 less than each side count
			var bottomRightIndex = bottomLeftIndex + 1;

			if (IsCellValid(gridPoints, topLeftIndex, topRightIndex, bottomLeftIndex, bottomRightIndex))
			{
				GridCell cell = new(
					gridPoints[topLeftIndex],
					gridPoints[topRightIndex],
					gridPoints[bottomLeftIndex],
					gridPoints[bottomRightIndex],
					false
				);

				AdjustCellBasedOnRaycast(ref cell);
				gridCells[x, y] = cell;
				gridCellsData[x, y] = new GridCellData();
			}
		}

		private bool IsCellValid(NativeArray<GridPoint> gridPoints,
			int topLeftIndex,
			int topRightIndex,
			int bottomLeftIndex,
			int bottomRightIndex)
		{
			return !(gridPoints[topLeftIndex].isVoid ||
			         gridPoints[topRightIndex].isVoid ||
			         gridPoints[bottomLeftIndex].isVoid ||
			         gridPoints[bottomRightIndex].isVoid);
		}

		private void AdjustCellBasedOnRaycast(ref GridCell cell)
		{
			Vector3 rayOrigin = new(cell.Center.posX, RAY_ORIGIN_HEIGHT, cell.Center.posZ);
			if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
			{
				if (Mathf.Abs(hit.point.y - cell.Center.posY) > 0.1f) cell.SetAllPointsToMinimumY();
			}
			else
			{
				cell.SetAllPointsToMinimumY();
			}
		}
	}
}