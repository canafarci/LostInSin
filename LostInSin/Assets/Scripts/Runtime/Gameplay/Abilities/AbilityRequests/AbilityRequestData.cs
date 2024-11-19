using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests
{
	public class AbilityRequestData : Poolable
	{
		public int DefaultActionPointCost;
		public int DynamicActionPointCost;

		public CharacterFacade User;
		public CharacterFacade Target;
		public Vector3 TargetPosition;
		public GridCell TargetGridCell;
		public List<GridCell> PathCells;

		public int totalActionPointCost => DynamicActionPointCost + DefaultActionPointCost;

		public override void OnGetFromPool()
		{
			User = null;
			PathCells = null;
			TargetPosition = default;
			TargetGridCell = null;
			Target = null;
			DynamicActionPointCost = 0;
			DefaultActionPointCost = 0;
		}
	}
}