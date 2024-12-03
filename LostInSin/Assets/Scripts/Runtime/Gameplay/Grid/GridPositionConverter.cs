using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public class GridPositionConverter : IGridPositionConverter
	{
		private readonly GridModel _gridModel;

		private GridPositionConverter(GridModel gridModel)
		{
			_gridModel = gridModel;
		}

		public bool GetCell(Vector3 worldPosition, out GridCell cell)
		{
			var row = Mathf.FloorToInt(worldPosition.x / _gridModel.gridCellWidth + _gridModel.gridRowOffset);
			var column = Mathf.FloorToInt(worldPosition.z / _gridModel.gridCellHeight + _gridModel.gridColumnOffset);

			var positionIsInsideGrid = false;
			cell = null;

			//cell count is 1 less than row and column size
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
	}
}