using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid.Data
{
	public class GridCell
	{
		public bool isOccupied { get; private set; }
		public Vector3 centerPosition { get; set; }
		public CharacterFacade character { get; private set; }

		public int x { get; set; }
		public int y { get; set; }

		public void SetAsOccupied(CharacterFacade facade)
		{
			if (isOccupied)
				throw new("Cell is already occupied!");

			isOccupied = true;
			character = facade;
		}

		public void SetAsUnoccupied()
		{
			if (!isOccupied)
				throw new("Cell is already unoccupied!");

			isOccupied = false;
		}
	}
}