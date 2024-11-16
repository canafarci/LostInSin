using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Raycast;
using LostInSin.Runtime.Raycast.Data;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public class GridGenerator
	{
		private readonly IGridRaycaster _raycaster;
		private readonly IGridPointsGenerator _pointsGenerator;
		private readonly IGridCellGenerator _cellGenerator;
		private readonly GridModel _gridModel;

		private GridGenerator(GridModel gridModel,
			IGridRaycaster raycaster,
			IGridPointsGenerator pointsGenerator,
			IGridCellGenerator cellGenerator)
		{
			_gridModel = gridModel;
			_raycaster = raycaster;
			_pointsGenerator = pointsGenerator;
			_cellGenerator = cellGenerator;
		}

		public void GenerateGrid()
		{
			GridRaycastData raycastData = CreateRaycastData();

			NativeArray<RaycastHit> raycastResults = _raycaster.PerformRaycasting(raycastData);
			NativeArray<GridPoint> gridPoints = _pointsGenerator.GenerateGridPoints(raycastResults);
			(GridCellData[,] cells, GridCell[,] data) gridCells = _cellGenerator.GenerateGridCells(gridPoints);

			_gridModel.SetGridCells(gridCells.cells, gridCells.data);
		}

		private GridRaycastData CreateRaycastData()
		{
			return new GridRaycastData
			{
				GridRowCount = _gridModel.gridRowCount,
				GridCellWidth = _gridModel.gridCellWidth,
				GridCellHeight = _gridModel.gridCellHeight,
				GridRowOffset = _gridModel.gridRowOffset,
				GridColumnOffset = _gridModel.gridColumnOffset,
				GridColumnCount = _gridModel.gridColumnCount
			};
		}
	}
}