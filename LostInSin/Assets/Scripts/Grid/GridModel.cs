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
        private readonly Data _data;
        public int GridCellWidth { get { return _data.GridData.GridXSize; } }
        public int GridCellHeight { get { return _data.GridData.GridYSize; } }
        public int GridRowCount { get { return _data.GridData.GridRowCount; } }
        public int GridColumnCount { get { return _data.GridData.GridColumnCount; } }
        public float GridRowOffset { get { return GridCellHeight * GridColumnCount / 2f; } }
        public float GridColumnOffset { get { return GridCellWidth * GridRowCount / 2f; } }

        private GridModel(Data data)
        {
            _data = data;
        }

        public void SetGridCells(GridCell[,] cells)
        {
            _gridCells = cells;

            DrawDebug(_gridCells);
        }
        public GridCell GetGridCell(int row, int column)
        {
            return _gridCells[row, column];
        }

        private void DrawDebug(GridCell[,] cells)
        {
            for (int x = 0; x < GridRowCount - 1; x++)
            {
                for (int y = 0; y < GridColumnCount - 1; y++)
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

        public class Data
        {
            public GridGenerationSO GridData;
        }
    }
}
