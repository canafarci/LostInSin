using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class GridPathFindingHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.GridPathFinding);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			if (abilityRequest.data.User.currentCell == null) return;
			if (abilityRequest.data.TargetGridCell == null) return;

			if (context.gridPathfinder.FindPath(abilityRequest.data.User.currentCell,
			                                    abilityRequest.data.TargetGridCell,
			                                    out List<GridCell> pathCells))
			{
				abilityRequest.data.PathCells = pathCells;
			}
		}
	}
}