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
		public int DynamicActionPointCost;
		public int DefaultActionPointCost;

		public int totalActionPointCost => DynamicActionPointCost + DefaultActionPointCost;

		public AbilityRequestData(int abilityDefaultActionPointCost)
		{
			DefaultActionPointCost = abilityDefaultActionPointCost;
		}

		public void Reset()
		{
			User = null;
			PathCells = null;
			TargetPosition = default;
			TargetGridCell = null;
			DynamicActionPointCost = 0;
			DefaultActionPointCost = 0;
		}
	}
}