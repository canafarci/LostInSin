using Unity.Collections;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public interface IGridPointsGenerator
	{
		NativeArray<GridPoint> GenerateGridPoints(NativeArray<RaycastHit> hitResults);
	}
}