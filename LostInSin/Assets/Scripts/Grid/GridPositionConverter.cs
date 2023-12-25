using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridPositionConverter
    {
        private readonly GridModel _gridModel;

        private GridPositionConverter(GridModel gridModel)
        {
            _gridModel = gridModel;
        }

        public Vector3 GetWorldPoint(int row, int column)
        {
            GridCellData cell = _gridModel.GetGridCellData(row, column);
            return cell.CenterPosition;
        }

        public Vector3 GetWorldPoint(GridCell cell)
        {
            return cell.Center.ToVector3();
        }

        public bool GetCell(Vector3 worldPosition, out GridCellData cellData)
        {
            int row = Mathf.FloorToInt(worldPosition.x / _gridModel.GridCellWidth + _gridModel.GridRowOffset);
            int column = Mathf.FloorToInt(worldPosition.z / _gridModel.GridCellHeight + _gridModel.GridColumnOffset);

            bool positionIsInsideGrid = false;
            cellData = null;

            //cell count is 1 less than row and column size
            if (row >= 0 && column >= 0 && row < _gridModel.GridRowCount - 1 && column < _gridModel.GridColumnCount - 1)
            {
                cellData = _gridModel.GetGridCellData(row, column);
                positionIsInsideGrid = true;
            }

            return positionIsInsideGrid;
        }
    }
}
