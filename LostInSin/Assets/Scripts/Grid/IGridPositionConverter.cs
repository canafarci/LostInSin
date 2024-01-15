using LostInSin.Grid.Data;
using UnityEngine;

namespace LostInSin.Grid
{
    public interface IGridPositionConverter
    {
        public bool GetCell(Vector3 worldPosition, out GridCellData cellData);
    }
}