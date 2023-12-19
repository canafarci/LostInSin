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
        private readonly Data _data;
        private readonly GridModel _gridModel;
        private int _groundLayerMask;

        public GridGenerator(Data data, GridModel gridModel)
        {
            _data = data;
            _gridModel = gridModel;
            _groundLayerMask = LayerMask.GetMask("Ground");
        }

        public void Initialize()
        {
            NativeArray<RaycastHit> hitResults = PerformRaycasting();
            NativeArray<GridPoint> gridPoints = GenerateGridPoints(hitResults);
            GridCell[,] gridCells = GenerateGridCells(gridPoints);

            int width = _data.GridData.GridWidth - 1;
            int height = _data.GridData.GridHeight - 1;
            _gridModel.SetGridCells(gridCells, width, height);
        }

        private NativeArray<RaycastHit> PerformRaycasting()
        {
            int gridWidth = _data.GridData.GridWidth;
            int gridHeight = _data.GridData.GridHeight;
            int gridSize = gridWidth * gridHeight;

            NativeArray<RaycastHit> hitResults = new NativeArray<RaycastHit>(gridSize, Allocator.TempJob);
            NativeArray<RaycastCommand> raycastCommands = new NativeArray<RaycastCommand>(gridSize, Allocator.TempJob);

            PrepareRaycastCommands(gridWidth, gridHeight, raycastCommands);

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
            int gridWidth = _data.GridData.GridWidth;
            int gridHeight = _data.GridData.GridHeight;

            int numCellsWide = gridWidth - 1;
            int numCellsHigh = gridHeight - 1;
            GridCell[,] gridCells = new GridCell[numCellsWide, numCellsHigh];

            for (int x = 0; x < numCellsWide; x++)
            {
                for (int y = 0; y < numCellsHigh; y++)
                {
                    ProcessCell(gridPoints, gridCells, x, y, gridWidth);
                }
            }

            gridPoints.Dispose();
            return gridCells;
        }

        private void ProcessCell(NativeArray<GridPoint> gridPoints, GridCell[,] gridCells, int x, int y, int gridWidth)
        {
            int topLeftIndex = x + y * gridWidth;
            int topRightIndex = topLeftIndex + 1;
            int bottomLeftIndex = topLeftIndex + gridWidth;
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

        private void PrepareRaycastCommands(int gridWidth, int gridHeight, NativeArray<RaycastCommand> commands)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    int index = x + y * gridWidth;
                    Vector3 gridRaycastOrigin = CreateGridRaycastOrigin(x, y, gridWidth, gridHeight);
                    QueryParameters queryParameters = new QueryParameters(_groundLayerMask, false, QueryTriggerInteraction.Ignore);
                    commands[index] = new RaycastCommand(gridRaycastOrigin, Vector3.down, queryParameters);
                }
            }
        }

        private Vector3 CreateGridRaycastOrigin(int x, int y, int gridWidth, int gridHeight)
        {
            float gridXSize = _data.GridData.GridXSize;
            float gridYSize = _data.GridData.GridYSize;

            float gridWidthOffset = gridXSize * gridWidth / 2f;
            float gridHeightOffset = gridYSize * gridHeight / 2f;

            return new Vector3(gridXSize * x - gridWidthOffset, _rayOriginHeight, gridYSize * y - gridHeightOffset);
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

        public class Data
        {
            public GridGenerationSO GridData;
        }
    }
}
