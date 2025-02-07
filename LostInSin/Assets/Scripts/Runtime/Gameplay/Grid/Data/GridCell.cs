using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Grid.Data
{
	public class GridCell
	{
		public bool isOccupied => character != null;
		public Vector3 centerPosition { get; set; }
		public CharacterFacade character { get; private set; }

		public int x { get; set; }
		public int y { get; set; }

		public void SetAsOccupied(CharacterFacade facade)
		{
			if (isOccupied)
				throw new("Cell is already occupied!");

			character = facade;
		}

		public void SetAsUnoccupied()
		{
			if (!isOccupied)
				throw new("Cell is already unoccupied!");

			character = null;
		}
	}
}