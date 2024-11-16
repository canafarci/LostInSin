using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Grid.DataObjects;

namespace LostInSin.Runtime.Grid
{
	public class GridModel
	{
		private readonly Data _data;
		private GridCell[,] _gridCellData;

		private GridModel(Data data)
		{
			_data = data;
		}

		public GridCellData[,] gridCells { get; private set; }

		public int gridCellWidth => _data.GridData.GridXSize;
		public int gridCellHeight => _data.GridData.GridYSize;
		public int gridRowCount => RoundToEvenNumber(_data.GridData.GridRowCount);
		public int gridColumnCount => RoundToEvenNumber(_data.GridData.GridColumnCount);
		public float gridRowOffset => gridCellHeight * gridColumnCount / 2f;
		public float gridColumnOffset => gridCellWidth * gridRowCount / 2f;

		public void SetGridCells(GridCellData[,] cells, GridCell[,] gridCellData)
		{
			gridCells = cells;
			_gridCellData = gridCellData;
		}

		public GridCell GetGridCellData(int row, int column)
		{
			GridCellData cellData = gridCells[row, column];
			GridCell data = _gridCellData[row, column];
			data.centerPosition = cellData.Center.ToVector3();
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