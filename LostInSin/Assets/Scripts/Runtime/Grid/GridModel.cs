using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Grid.DataObjects;

namespace LostInSin.Runtime.Grid
{
	public class GridModel
	{
		private readonly Data _data;
		private GridCellData[,] _gridCellData;

		private GridModel(Data data)
		{
			_data = data;
		}

		public GridCell[,] gridCells { get; private set; }

		public int gridCellWidth => _data.GridData.GridXSize;
		public int gridCellHeight => _data.GridData.GridYSize;
		public int gridRowCount => RoundToEvenNumber(_data.GridData.GridRowCount);
		public int gridColumnCount => RoundToEvenNumber(_data.GridData.GridColumnCount);
		public float gridRowOffset => gridCellHeight * gridColumnCount / 2f;
		public float gridColumnOffset => gridCellWidth * gridRowCount / 2f;

		public void SetGridCells(GridCell[,] cells, GridCellData[,] gridCellData)
		{
			gridCells = cells;
			_gridCellData = gridCellData;
		}

		public GridCellData GetGridCellData(int row, int column)
		{
			GridCell cell = gridCells[row, column];
			GridCellData data = _gridCellData[row, column];
			data.centerPosition = cell.Center.ToVector3();
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