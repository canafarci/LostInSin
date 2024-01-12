using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid
{
    public interface IGridCellGenerator
    {
        (GridCell[,] cells,  GridCellData[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints);
    }
}
