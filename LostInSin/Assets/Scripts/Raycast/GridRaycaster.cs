using LostInSin.Raycast.Data;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace LostInSin.Raycast
{
    public class GridRaycaster : IGridRaycaster
    {
        private const float RayOriginHeight = 10f;
        private readonly int _groundLayerMask;

        public GridRaycaster()
        {
            _groundLayerMask = LayerMask.GetMask("Ground");
        }

        public NativeArray<RaycastHit> PerformRaycasting(GridRaycastData raycastData)
        {
            int rowCount = raycastData.GridRowCount + 1;
            int columnCount = raycastData.GridColumnCount + 1;
            int gridSize = rowCount * columnCount;

            NativeArray<RaycastHit> hitResults = new(gridSize, Allocator.TempJob);
            NativeArray<RaycastCommand> raycastCommands = new(gridSize, Allocator.TempJob);

            PrepareRaycastCommands(rowCount, columnCount, raycastData, raycastCommands);

            JobHandle raycastHandle = RaycastCommand.ScheduleBatch(raycastCommands, hitResults, 1);
            raycastHandle.Complete();

            raycastCommands.Dispose();
            return hitResults;
        }

        private void PrepareRaycastCommands(int rowCount,
                                            int columnCount,
                                            GridRaycastData raycastData,
                                            NativeArray<RaycastCommand> commands)
        {
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int index = row + column * rowCount;
                    Vector3 gridRaycastOrigin = CreateGridRaycastOrigin(row, column, raycastData);
                    QueryParameters queryParameters = new(_groundLayerMask, false, QueryTriggerInteraction.Ignore);
                    commands[index] = new RaycastCommand(gridRaycastOrigin, Vector3.down, queryParameters);
                }
            }
        }

        private Vector3 CreateGridRaycastOrigin(int row, int column, GridRaycastData raycastData)
        {
            float cellWidth = raycastData.GridCellWidth;
            float cellHeight = raycastData.GridCellHeight;

            float gridRowOffset = raycastData.GridRowOffset;
            float gridColumnOffset = raycastData.GridColumnOffset;

            return new Vector3(cellWidth * row - gridRowOffset, RayOriginHeight, cellHeight * column - gridColumnOffset);
        }
    }
}