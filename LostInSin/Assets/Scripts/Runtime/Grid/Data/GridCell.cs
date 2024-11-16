using UnityEngine;

namespace LostInSin.Runtime.Grid.Data
{
	public class GridCell
	{
		public bool isOccupied => _isOccupied;

		public Vector3 centerPosition
		{
			get => _centerPosition;
			set => _centerPosition = value;
		}

		private bool _isOccupied = false;
		private Vector3 _centerPosition;

		public void SetAsOccupied()
		{
			if (_isOccupied)
				throw new System.Exception("Cell is already occupied!");

			_isOccupied = true;
		}

		public void SetAsUnoccupied()
		{
			if (!_isOccupied)
				throw new System.Exception("Cell is already unoccupied!");

			_isOccupied = false;
		}
	}
}