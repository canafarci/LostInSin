using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class AbilityRequestData : Poolable
	{
		public int DefaultActionPointCost;
		public int DynamicActionPointCost;

		public CharacterFacade User;
		public CharacterFacade TargetCharacter;
		public Vector3 TargetPosition;
		public GridCell TargetGridCell;
		public List<GridCell> TargetGridCells;
		public List<GridCell> PathCells;

		public int totalActionPointCost => DynamicActionPointCost + DefaultActionPointCost;

		public override void OnGetFromPool()
		{
			User = null;
			PathCells = null;
			TargetPosition = default;
			TargetGridCell = null;
			TargetGridCells = null;
			TargetCharacter = null;
			DynamicActionPointCost = 0;
			DefaultActionPointCost = 0;
		}
	}
}