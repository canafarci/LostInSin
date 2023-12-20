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
            GridCell cell = _gridModel.GetGridCell(row, column);
            return cell.Center.ToVector3();
        }

        public Vector3 GetWorldPoint(GridCell cell)
        {
            return cell.Center.ToVector3();
        }

        public bool GetCellCenterPoint(Vector3 worldPosition, out Vector3 cellPosition)
        {
            cellPosition = default;
            bool foundPosition = false;

            if (GetCell(worldPosition, out GridCell cell))
            {
                cellPosition = cell.Center.ToVector3();
                foundPosition = true;
            }

            return foundPosition;
        }

        public bool GetCell(Vector3 worldPosition, out GridCell cell)
        {
            int row = Mathf.FloorToInt(worldPosition.x / _gridModel.GridCellWidth + _gridModel.GridRowOffset);
            int column = Mathf.FloorToInt(worldPosition.z / _gridModel.GridCellHeight + _gridModel.GridColumnOffset);

            bool positionIsInsideGrid = false;
            cell = default;

            //cell count is 1 less than row and column size
            if (row >= 0 && column >= 0 && row < _gridModel.GridRowCount - 1 && column < _gridModel.GridColumnCount - 1)
            {
                cell = _gridModel.GetGridCell(row, column);
                positionIsInsideGrid = true;
            }

            return positionIsInsideGrid;
        }
    }
}
