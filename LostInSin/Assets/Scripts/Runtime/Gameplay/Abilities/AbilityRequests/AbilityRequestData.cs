using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class AbilityRequestData
	{
		public CharacterFacade User;
		public Vector3 TargetPosition;
		public GridCell TargetGridCell;
		public List<GridCell> PathCells;

		public void Reset()
		{
			User = null;
			PathCells = null;
			TargetPosition = default;
			TargetGridCell = null;
		}
	}
}