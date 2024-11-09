using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public class GridPositionConverter : IGridPositionConverter
	{
		private readonly GridModel _gridModel;

		private GridPositionConverter(GridModel gridModel)
		{
			_gridModel = gridModel;
		}

		public bool GetCell(Vector3 worldPosition, out GridCellData cellData)
		{
			var row = Mathf.FloorToInt(worldPosition.x / _gridModel.gridCellWidth + _gridModel.gridRowOffset);
			var column = Mathf.FloorToInt(worldPosition.z / _gridModel.gridCellHeight + _gridModel.gridColumnOffset);

			var positionIsInsideGrid = false;
			cellData = null;

			//cell count is 1 less than row and column size
			if (row >= 0 && column >= 0 && row < _gridModel.gridRowCount - 1 && column < _gridModel.gridColumnCount - 1)
			{
				cellData = _gridModel.GetGridCellData(row, column);
				positionIsInsideGrid = true;
			}

			return positionIsInsideGrid;
		}

		public Vector3 GetWorldPoint(int row, int column)
		{
			GridCellData cell = _gridModel.GetGridCellData(row, column);
			return cell.centerPosition;
		}

		public Vector3 GetWorldPoint(GridCell cell)
		{
			return cell.Center.ToVector3();
		}
	}
}