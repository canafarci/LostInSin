using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid
{
    public class GridGenerator : IInitializable
    {
        private const float _rayOriginHeight = 10f;
        private readonly GridModel _gridModel;
        private int _groundLayerMask;

        private GridGenerator(GridModel gridModel)
        {
            _gridModel = gridModel;
            _groundLayerMask = LayerMask.GetMask("Ground");
        }

        public void Initialize()
        {
            NativeArray<RaycastHit> hitResults = PerformRaycasting();
            NativeArray<GridPoint> gridPoints = GenerateGridPoints(hitResults);
            GridCell[,] gridCells = GenerateGridCells(gridPoints);

            _gridModel.SetGridCells(gridCells);
        }

        private NativeArray<RaycastHit> PerformRaycasting()
        {
            int gridRowCount = _gridModel.GridRowCount;
            int gridColumnCount = _gridModel.GridColumnCount;
            int gridSize = gridRowCount * gridColumnCount;

            NativeArray<RaycastHit> hitResults = new NativeArray<RaycastHit>(gridSize, Allocator.TempJob);
            NativeArray<RaycastCommand> raycastCommands = new NativeArray<RaycastCommand>(gridSize, Allocator.TempJob);

            PrepareRaycastCommands(gridRowCount, gridColumnCount, raycastCommands);

            JobHandle raycastHandle = RaycastCommand.ScheduleBatch(raycastCommands, hitResults, 1);
            raycastHandle.Complete();

            raycastCommands.Dispose();
            return hitResults;
        }

        private NativeArray<GridPoint> GenerateGridPoints(NativeArray<RaycastHit> hitResults)
        {
            NativeArray<GridPoint> gridPoints = new NativeArray<GridPoint>(hitResults.Length, Allocator.TempJob);

            CreateGridArrayJob createGridArrayJob = new CreateGridArrayJob()
            {
                HitResults = hitResults,
                GridPoints = gridPoints
            };

            JobHandle gridPointCreationHandle = createGridArrayJob.Schedule(hitResults.Length, default);
            gridPointCreationHandle.Complete();

            hitResults.Dispose();
            return gridPoints;
        }

        private GridCell[,] GenerateGridCells(NativeArray<GridPoint> gridPoints)
        {
            int numCellsRow = _gridModel.GridRowCount - 1;
            int numCellsColumn = _gridModel.GridColumnCount - 1;

            GridCell[,] gridCells = new GridCell[numCellsRow, numCellsColumn];

            for (int x = 0; x < numCellsRow; x++)
            {
                for (int y = 0; y < numCellsColumn; y++)
                {
                    ProcessCell(gridPoints, gridCells, x, y);
                }
            }

            gridPoints.Dispose();
            return gridCells;
        }

        private void ProcessCell(NativeArray<GridPoint> gridPoints, GridCell[,] gridCells, int x, int y)
        {
            int topLeftIndex = x + y * _gridModel.GridRowCount;
            int topRightIndex = topLeftIndex + 1;
            int bottomLeftIndex = topLeftIndex + _gridModel.GridRowCount;
            int bottomRightIndex = bottomLeftIndex + 1;

            if (IsCellValid(gridPoints, topLeftIndex, topRightIndex, bottomLeftIndex, bottomRightIndex))
            {
                GridCell cell = new GridCell(
                    gridPoints[topLeftIndex],
                    gridPoints[topRightIndex],
                    gridPoints[bottomLeftIndex],
                    gridPoints[bottomRightIndex],
                    false
                );

                AdjustCellBasedOnRaycast(ref cell);
                gridCells[x, y] = cell;
            }
        }

        private bool IsCellValid(NativeArray<GridPoint> gridPoints, int topLeftIndex, int topRightIndex, int bottomLeftIndex, int bottomRightIndex)
        {
            return !(gridPoints[topLeftIndex].IsVoid || gridPoints[topRightIndex].IsVoid ||
                     gridPoints[bottomLeftIndex].IsVoid || gridPoints[bottomRightIndex].IsVoid);
        }

        private void AdjustCellBasedOnRaycast(ref GridCell cell)
        {
            Vector3 rayOrigin = new Vector3(cell.Center.PosX, _rayOriginHeight, cell.Center.PosZ);
            if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
            {
                if (Mathf.Abs(hit.point.y - cell.Center.PosY) > 0.1f)
                {
                    cell.SetAllPointsToMinimumY();
                }
            }
            else
            {
                cell.SetAllPointsToMinimumY();
            }
        }

        private void PrepareRaycastCommands(int rowCount, int columnCount, NativeArray<RaycastCommand> commands)
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int index = row + column * rowCount;
                    Vector3 gridRaycastOrigin = CreateGridRaycastOrigin(row, column);
                    QueryParameters queryParameters = new QueryParameters(_groundLayerMask, false, QueryTriggerInteraction.Ignore);
                    commands[index] = new RaycastCommand(gridRaycastOrigin, Vector3.down, queryParameters);
                }
            }
        }

        private Vector3 CreateGridRaycastOrigin(int row, int column)
        {
            float cellWidth = _gridModel.GridCellWidth;
            float cellHeight = _gridModel.GridCellHeight;

            float gridRowOffset = _gridModel.GridRowOffset;
            float gridColumnOffset = _gridModel.GridColumnOffset;

            return new Vector3(cellWidth * row - gridRowOffset,
                                _rayOriginHeight,
                                cellHeight * column - gridColumnOffset);
        }

        private struct CreateGridArrayJob : IJobFor
        {
            [ReadOnly] public NativeArray<RaycastHit> HitResults;
            public NativeArray<GridPoint> GridPoints;

            public void Execute(int index)
            {
                RaycastHit hit = HitResults[index];
                GridPoints[index] = hit.distance > 0 ? new GridPoint(hit.point.x, hit.point.y, hit.point.z, false) : new GridPoint();
            }
        }


    }
}
