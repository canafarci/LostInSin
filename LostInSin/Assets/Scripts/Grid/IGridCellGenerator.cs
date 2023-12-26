using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid
{
    public interface IGridCellGenerator
    {
        (GridCell[,], GridCellData[,]) GenerateGridCells(NativeArray<GridPoint> gridPoints);
    }
}
