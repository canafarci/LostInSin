using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Zenject;

namespace LostInSin.Grid
{
    public class GridGenerator : IInitializable
    {
        private readonly Data _data;
        private readonly GridModel _gridModel;
        private int _groundLayerMask = 1 << 3;

        private GridGenerator(Data data, GridModel gridModel)
        {
            _data = data;
            _gridModel = gridModel;
        }

        public void Initialize()
        {
            int gridWidth = _data.GridData.GridWidth;
            int gridHeight = _data.GridData.GridHeight;
            int gridSize = gridWidth * gridHeight;

            NativeArray<RaycastHit> hitResults = new NativeArray<RaycastHit>(gridSize, Allocator.TempJob);
            NativeArray<RaycastCommand> commands = new NativeArray<RaycastCommand>(gridSize, Allocator.TempJob);

            // Prepare RaycastCommands
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    int index = x + y * gridWidth;
                    Vector3 gridRaycastOrigin = CreateGridRaycastOrigin(x, y, gridWidth, gridHeight);
                    Ray gridRay = CreateGroundDirectionRay(gridRaycastOrigin);
                    QueryParameters queryParameters = new QueryParameters(_groundLayerMask, false, QueryTriggerInteraction.Ignore, false);
                    commands[index] = new RaycastCommand(gridRay.origin, gridRay.direction, queryParameters);
                }
            }

            JobHandle handle = RaycastCommand.ScheduleBatch(commands, hitResults, 1, default);

            handle.Complete(); // Wait for the job to complete before accessing the results
            commands.Dispose();

            NativeArray<GridPoint> gridPoints = new NativeArray<GridPoint>(gridSize, Allocator.TempJob);

            CreateGridArrayJob createGridArrayJob = new CreateGridArrayJob()
            {
                HitResults = hitResults,
                GridPoints = gridPoints
            };

            JobHandle gridHandle = createGridArrayJob.Schedule(gridSize, handle);
            gridHandle.Complete();

            GridPoint[] gridPointsArray = new GridPoint[gridSize];
            gridPoints.CopyTo(gridPointsArray);

            _gridModel.SetGrid(gridPointsArray);

            hitResults.Dispose();
            gridPoints.Dispose();
        }

        private Vector3 CreateGridRaycastOrigin(int x, int y, int gridWidth, int gridHeight)
        {
            float gridXSize = _data.GridData.GridXSize;
            float gridYSize = _data.GridData.GridYSize;

            float gridWidthOffset = gridXSize * gridWidth / 2f;
            float gridHeightOffset = gridYSize * gridHeight / 2f;

            float gridPositionX = gridXSize * x - gridWidthOffset;
            float gridPositionY = gridYSize * y - gridHeightOffset;

            Vector3 gridRaycastOrigin = new Vector3(gridPositionX, 0f, gridPositionY);
            return gridRaycastOrigin;
        }

        private Ray CreateGroundDirectionRay(Vector3 rayOrigin)
        {
            rayOrigin += Vector3.up * 10f;
            Vector3 rayDirection = new Vector3(0, -1, 0);
            Ray groundDirectionRay = new Ray(rayOrigin, rayDirection);
            return groundDirectionRay;
        }

        public struct CreateGridArrayJob : IJobFor
        {
            public NativeArray<RaycastHit> HitResults;
            public NativeArray<GridPoint> GridPoints;
            public void Execute(int index)
            {
                RaycastHit hit = HitResults[index];

                GridPoint point;

                if (hit.distance > 0)
                {
                    Vector3 hitPoint = hit.point;

                    point = new GridPoint(
                        hitPoint.x,
                        hitPoint.y,
                        hitPoint.z,
                        false
                    );
                }
                else
                {
                    point = new GridPoint();
                }

                GridPoints[index] = point;
            }
        }
        public class Data
        {
            public GridGenerationSO GridData;
        }
    }
}