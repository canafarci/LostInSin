using LostInSin.Grid.Data;
using Unity.Collections;

namespace LostInSin.Grid
{
    public interface IGridCellGenerator
    {
        (GridCell[,] cells, GridCellData[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints);
    }
}