using LostInSin.Runtime.Gameplay.Grid.Data;
using Unity.Collections;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public interface IGridCellGenerator
	{
		(GridCellData[,] cells, GridCell[,] data ) GenerateGridCells(NativeArray<GridPoint> gridPoints);
	}
}