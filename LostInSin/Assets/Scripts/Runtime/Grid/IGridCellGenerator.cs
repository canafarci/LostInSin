using LostInSin.Runtime.Grid.Data;
using Unity.Collections;

namespace LostInSin.Runtime.Grid
{
    public interface IGridCellGenerator
    {
        (GridCell[,] cells, GridCellData[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints);
    }
}