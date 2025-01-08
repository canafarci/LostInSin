using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class GridMovementHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			//Movement + GridPositionRaycasted means "move to grid cell"
			return requestType.HasFlag(AbilityRequestType.Movement)
			       && requestType.HasFlag(AbilityRequestType.GridPositionRaycasted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			// Do a raycast for an empty cell, set it as TargetGridCell
			if (context.RaycastRequest != null && !context.RaycastRequest.isProcessed)
			{
				if (context.playerRaycaster.TryRaycastForEmptyGridCell(
				                                                       abilityRequest,
				                                                       ref context.RaycastRequest,
				                                                       out GridCell cell))
				{
					abilityRequest.data.TargetGridCell = cell;
				}
			}
		}
	}
}