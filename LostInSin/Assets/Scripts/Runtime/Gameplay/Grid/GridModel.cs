using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Grid.DataObjects;

namespace LostInSin.Runtime.Gameplay.Grid
{
	public class GridModel
	{
		private readonly Data _data;
		private GridCell[,] _gridCells;

		private GridModel(Data data)
		{
			_data = data;
		}

		public GridCellData[,] gridCellsData { get; private set; }

		public int gridCellWidth => _data.GridData.GridXSize;
		public int gridCellHeight => _data.GridData.GridYSize;
		public int gridRowCount => RoundToEvenNumber(_data.GridData.GridRowCount);
		public int gridColumnCount => RoundToEvenNumber(_data.GridData.GridColumnCount);
		public float gridRowOffset => gridCellHeight * gridColumnCount / 2f;
		public float gridColumnOffset => gridCellWidth * gridRowCount / 2f;

		public void SetGridCells(GridCellData[,] cells, GridCell[,] gridCellData)
		{
			gridCellsData = cells;
			_gridCells = gridCellData;

			SetGridCellData();
		}

		public GridCell GetGridCell(int row, int column)
		{
			GridCell cell = _gridCells[row, column];
			return cell;
		}

		private void SetGridCellData()
		{
			for (int x = 0; x < _gridCells.GetLength(0); x++)
			{
				for (int y = 0; y < _gridCells.GetLength(1); y++)
				{
					GridCellData cellData = gridCellsData[x, y];
					GridCell cell = _gridCells[x, y];

					cell.centerPosition = cellData.Center.ToVector3();
					cell.x = x;
					cell.y = y;
				}
			}
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