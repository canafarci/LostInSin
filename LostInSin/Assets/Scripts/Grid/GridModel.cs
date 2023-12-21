using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridModel
    {
        private GridCell[,] _gridCells;
        private readonly Data _data;
        public GridCell[,] GridCells { get { return _gridCells; } }
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
        }

        public GridCell GetGridCell(int row, int column)
        {
            return _gridCells[row, column];
        }

        public class Data
        {
            public GridGenerationSO GridData;
        }
    }
}
