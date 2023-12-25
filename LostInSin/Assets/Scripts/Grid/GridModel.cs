using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridModel
    {
        private GridCell[,] _gridCells;
        private GridCellData[,] _gridCellData;
        private readonly Data _data;
        public GridCell[,] GridCells { get { return _gridCells; } }
        public int GridCellWidth { get { return _data.GridData.GridXSize; } }
        public int GridCellHeight { get { return _data.GridData.GridYSize; } }
        public int GridRowCount { get { return RoundToEvenNumber(_data.GridData.GridRowCount); } }
        public int GridColumnCount { get { return RoundToEvenNumber(_data.GridData.GridColumnCount); } }
        public float GridRowOffset { get { return GridCellHeight * GridColumnCount / 2f; } }
        public float GridColumnOffset { get { return GridCellWidth * GridRowCount / 2f; } }

        private GridModel(Data data)
        {
            _data = data;
        }

        public void SetGridCells(GridCell[,] cells, GridCellData[,] gridCellData)
        {
            _gridCells = cells;
            _gridCellData = gridCellData;
        }

        public GridCellData GetGridCellData(int row, int column)
        {
            GridCell cell = _gridCells[row, column];
            GridCellData data = _gridCellData[row, column];
            data.CenterPosition = cell.Center.ToVector3();
            return data;
        }

        private int RoundToEvenNumber(int number)
        {
            if (number % 2 != 0)
                number -= 1;
            return number;
        }

        public class Data
        {
            public GridGenerationSO GridData;
        }
    }
}
