using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public interface IGridPointsGenerator
	{
		NativeArray<GridPoint> GenerateGridPoints(NativeArray<RaycastHit> hitResults);
	}
}