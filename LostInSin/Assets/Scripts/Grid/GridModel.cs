namespace LostInSin.Grid
{
    public class GridModel
    {
        private readonly Data _data;
        private GridCellData[,] _gridCellData;

        private GridModel(Data data)
        {
            _data = data;
        }

        public GridCell[,] GridCells { get; private set; }

        public int GridCellWidth => _data.GridData.GridXSize;
        public int GridCellHeight => _data.GridData.GridYSize;
        public int GridRowCount => RoundToEvenNumber(_data.GridData.GridRowCount);
        public int GridColumnCount => RoundToEvenNumber(_data.GridData.GridColumnCount);
        public float GridRowOffset => GridCellHeight * GridColumnCount / 2f;
        public float GridColumnOffset => GridCellWidth * GridRowCount / 2f;

        public void SetGridCells(GridCell[,] cells, GridCellData[,] gridCellData)
        {
            GridCells = cells;
            _gridCellData = gridCellData;
        }

        public GridCellData GetGridCellData(int row, int column)
        {
            GridCell cell = GridCells[row, column];
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