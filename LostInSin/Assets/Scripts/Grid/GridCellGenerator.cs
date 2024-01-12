using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid
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
            int numCellsRow = _gridModel.GridRowCount;
            int numCellsColumn = _gridModel.GridColumnCount;

            GridCell[,] gridCells = new GridCell[numCellsRow, numCellsColumn];
            GridCellData[,] gridCellsData = new GridCellData[numCellsRow, numCellsColumn];

            for (int x = 0; x < numCellsRow; x++)
            for (int y = 0; y < numCellsColumn; y++)
                ProcessCell(gridPoints, gridCells, gridCellsData, x, y);

            gridPoints.Dispose();
            return (gridCells, gridCellsData
                );
        }

        private void ProcessCell(NativeArray<GridPoint> gridPoints, GridCell[,] gridCells, GridCellData[,] gridCellsData
            , int x, int y)
        {
            int topLeftIndex =
                x + y * (_gridModel.GridRowCount +
                         1); // add one to side length value, as grid cells count is  1 less than each side count
            int topRightIndex = topLeftIndex + 1;
            int bottomLeftIndex =
                topLeftIndex + _gridModel.GridRowCount +
                1; // add one to side length value, as grid cells count is  1 less than each side count
            int bottomRightIndex = bottomLeftIndex + 1;

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

        private bool IsCellValid(NativeArray<GridPoint> gridPoints, int topLeftIndex, int topRightIndex
            , int bottomLeftIndex, int bottomRightIndex)
        {
            return !(gridPoints[topLeftIndex].IsVoid || gridPoints[topRightIndex].IsVoid ||
                     gridPoints[bottomLeftIndex].IsVoid || gridPoints[bottomRightIndex].IsVoid);
        }

        private void AdjustCellBasedOnRaycast(ref GridCell cell)
        {
            Vector3 rayOrigin = new(cell.Center.PosX, RAY_ORIGIN_HEIGHT, cell.Center.PosZ);
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
            {
                if (Mathf.Abs(hit.point.y - cell.Center.PosY) > 0.1f) cell.SetAllPointsToMinimumY();
            }
            else
            {
                cell.SetAllPointsToMinimumY();
            }
        }
    }
}