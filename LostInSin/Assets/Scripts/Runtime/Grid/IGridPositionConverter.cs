using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Grid
{
	public interface IGridPositionConverter
	{
		public bool GetCell(Vector3 worldPosition, out GridCell cell);
	}
}