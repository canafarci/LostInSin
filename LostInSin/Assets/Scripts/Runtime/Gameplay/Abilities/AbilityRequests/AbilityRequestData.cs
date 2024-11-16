using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class AbilityRequestData
	{
		public CharacterFacade User;
		public Vector3 TargetPosition;
		public GridCellData TargetGridCell;

		public void Reset()
		{
			User = null;
			TargetPosition = default;
			TargetGridCell = default;
		}
	}
}