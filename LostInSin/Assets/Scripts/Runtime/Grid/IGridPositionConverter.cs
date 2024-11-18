using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public interface IGridPositionConverter
	{
		public bool GetCell(Vector3 worldPosition, out GridCell cell);
		public Vector3 GetWorldPoint(int row, int column);
		public GridCell GetCell(int row, int column);
		public Vector3 GetWorldPoint(GridCellData cellData);
	}
}