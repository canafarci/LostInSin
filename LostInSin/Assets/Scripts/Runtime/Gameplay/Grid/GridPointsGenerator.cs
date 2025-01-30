using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public class GridPointsGenerator : IGridPointsGenerator
	{
		public NativeArray<GridPoint> GenerateGridPoints(NativeArray<RaycastHit> hitResults)
		{
			using (hitResults)
			{
				NativeArray<GridPoint> gridPoints = new(hitResults.Length, Allocator.TempJob);

				CreateGridArrayJob createGridArrayJob = new()
				{
					HitResults = hitResults, GridPoints = gridPoints
				};

				JobHandle gridPointCreationHandle =
					createGridArrayJob.Schedule(hitResults.Length, 64); // 64 is the batch size
				gridPointCreationHandle.Complete();

				return gridPoints;
			}
		}

		private struct CreateGridArrayJob : IJobParallelFor
		{
			[ReadOnly] public NativeArray<RaycastHit> HitResults;
			public NativeArray<GridPoint> GridPoints;

			public void Execute(int index)
			{
				RaycastHit hit = HitResults[index];
				GridPoints[index] = hit.distance > 0
					? new(hit.point.x, hit.point.y, hit.point.z, false)
					: new GridPoint();
			}
		}
	}
}