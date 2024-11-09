using LostInSin.Runtime.Raycast.Data;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace LostInSin.Runtime.Raycast
{
	public class GridRaycaster : IGridRaycaster
	{
		private const float RAY_ORIGIN_HEIGHT = 10f;
		private readonly int _groundLayerMask;

		public GridRaycaster()
		{
			_groundLayerMask = LayerMask.GetMask("Ground");
		}

		public NativeArray<RaycastHit> PerformRaycasting(GridRaycastData raycastData)
		{
			var rowCount = raycastData.GridRowCount + 1;
			var columnCount = raycastData.GridColumnCount + 1;
			var gridSize = rowCount * columnCount;

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
			for (var row = 0; row < rowCount; row++)
			for (var column = 0; column < columnCount; column++)
			{
				var index = row + column * rowCount;
				Vector3 gridRaycastOrigin = CreateGridRaycastOrigin(row, column, raycastData);
				QueryParameters queryParameters = new(_groundLayerMask, false, QueryTriggerInteraction.Ignore);
				commands[index] = new RaycastCommand(gridRaycastOrigin, Vector3.down, queryParameters);
			}
		}

		private Vector3 CreateGridRaycastOrigin(int row, int column, GridRaycastData raycastData)
		{
			var cellWidth = raycastData.GridCellWidth;
			var cellHeight = raycastData.GridCellHeight;

			var gridRowOffset = raycastData.GridRowOffset;
			var gridColumnOffset = raycastData.GridColumnOffset;

			return new Vector3(cellWidth * row - gridRowOffset, RAY_ORIGIN_HEIGHT,
				cellHeight * column - gridColumnOffset);
		}
	}
}