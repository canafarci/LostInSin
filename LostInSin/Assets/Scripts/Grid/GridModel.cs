using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridModel
    {
        private GridCell[,] _gridCells;

        public void SetGridCells(GridCell[,] cells, int width, int height)
        {
            _gridCells = cells;

            DrawDebug(_gridCells, width, height);
        }

        private void DrawDebug(GridCell[,] cells, int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    GridCell cell = cells[x, y];
#if UNITY_EDITOR
                    if (cell.IsInvalid) continue;
                    Debug.DrawLine(cell.TopLeft.ToVector3(), cell.TopRight.ToVector3(), Color.blue, Mathf.Infinity);
                    Debug.DrawLine(cell.TopRight.ToVector3(), cell.BottomRight.ToVector3(), Color.blue, Mathf.Infinity);
                    Debug.DrawLine(cell.BottomRight.ToVector3(), cell.BottomLeft.ToVector3(), Color.blue, Mathf.Infinity);
                    Debug.DrawLine(cell.BottomLeft.ToVector3(), cell.TopLeft.ToVector3(), Color.blue, Mathf.Infinity);
                    Debug.DrawLine(cell.BottomLeft.ToVector3(), cell.TopRight.ToVector3(), Color.blue, Mathf.Infinity);
#endif
                }
            }
        }
    }
}
