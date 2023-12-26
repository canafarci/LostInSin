using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid
{
    public interface IGridPointsGenerator
    {
        NativeArray<GridPoint> GenerateGridPoints(NativeArray<RaycastHit> hitResults);
    }
}
