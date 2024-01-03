using System.Collections.Generic;
using UnityEngine;

namespace LostInSin.Grid
{
    public class GridModel
    {
        private GridCell[,] _gridCells;
        private GridCellData[,] _gridCellData;
        private readonly Data _data;
        public GridCell[,] GridCells => _gridCells;
        public int GridCellWidth => _data.GridData.GridXSize;
        public int GridCellHeight => _data.GridData.GridYSize;
        public int GridRowCount => RoundToEvenNumber(_data.GridData.GridRowCount);
        public int GridColumnCount => RoundToEvenNumber(_data.GridData.GridColumnCount);
        public float GridRowOffset => GridCellHeight * GridColumnCount / 2f;
        public float GridColumnOffset => GridCellWidth * GridRowCount / 2f;

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
