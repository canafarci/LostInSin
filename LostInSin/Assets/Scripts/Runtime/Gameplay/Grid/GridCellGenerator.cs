using LostInSin.Runtime.Gameplay.Grid.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid
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

		public (GridCellData[,] cells, GridCell[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints)
		{
			using (gridPoints)
			{
				int numCellsRow = _gridModel.gridRowCount;
				int numCellsColumn = _gridModel.gridColumnCount;

				GridCellData[,] gridCells = new GridCellData[numCellsRow, numCellsColumn];
				GridCell[,] gridCellsData = new GridCell[numCellsRow, numCellsColumn];

				for (int x = 0; x < numCellsRow; x++)
					for (int y = 0; y < numCellsColumn; y++)
						ProcessCell(gridPoints, gridCells, gridCellsData, x, y);

				return (gridCells, gridCellsData);
			}
		}

		private void ProcessCell(NativeArray<GridPoint> gridPoints,
			GridCellData[,] gridCells,
			GridCell[,] gridCellsData,
			int x,
			int y)
		{
			int topLeftIndex =
				x +
				y *
				(_gridModel.gridRowCount +
				 1); // add one to side length value, as grid cells count is  1 less than each side count
			int topRightIndex = topLeftIndex + 1;
			int bottomLeftIndex =
				topLeftIndex +
				_gridModel.gridRowCount +
				1; // add one to side length value, as grid cells count is  1 less than each side count
			int bottomRightIndex = bottomLeftIndex + 1;

			if (IsCellValid(gridPoints, topLeftIndex, topRightIndex, bottomLeftIndex, bottomRightIndex))
			{
				GridCellData cellData = new(
				                            gridPoints[topLeftIndex],
				                            gridPoints[topRightIndex],
				                            gridPoints[bottomLeftIndex],
				                            gridPoints[bottomRightIndex],
				                            false
				                           );

				AdjustCellBasedOnRaycast(ref cellData);
				gridCells[x, y] = cellData;
				gridCellsData[x, y] = new();
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

		private void AdjustCellBasedOnRaycast(ref GridCellData cellData)
		{
			Vector3 rayOrigin = new(cellData.Center.posX, RAY_ORIGIN_HEIGHT, cellData.Center.posZ);
			if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
			{
				if (Mathf.Abs(hit.point.y - cellData.Center.posY) > 0.1f) cellData.SetAllPointsToMinimumY();
			}
			else
			{
				cellData.SetAllPointsToMinimumY();
			}
		}
	}
}