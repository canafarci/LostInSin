using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid.Data
{
	public class GridCell
	{
		public bool isOccupied { get; private set; }
		public Vector3 centerPosition { get; set; }

		public int x { get; set; }
		public int y { get; set; }

		public void SetAsOccupied()
		{
			if (isOccupied)
				throw new("Cell is already occupied!");

			isOccupied = true;
		}

		public void SetAsUnoccupied()
		{
			if (!isOccupied)
				throw new("Cell is already unoccupied!");

			isOccupied = false;
		}
	}
}