using LostInSin.Runtime.Grid.Data;
using Unity.Collections;

namespace LostInSin.Runtime.Grid
{
	public interface IGridCellGenerator
	{
		(GridCellData[,] cells, GridCell[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints);
	}
}