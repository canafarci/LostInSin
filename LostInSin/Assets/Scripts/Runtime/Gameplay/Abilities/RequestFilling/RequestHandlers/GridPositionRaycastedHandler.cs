using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid.Data;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class GridPositionRaycastedHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.GridPositionRaycasted) && !requestType.HasFlag(AbilityRequestType.Movement);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			// Do a raycast for a cell, set it as TargetGridCell if it not movement type
			if (context.RaycastRequest != null && !context.RaycastRequest.isProcessed)
			{
				if (context.playerRaycaster.TryRaycastForGridCell(abilityRequest,
				                                                  ref context.RaycastRequest,
				                                                  out GridCell cell))
				{
					abilityRequest.data.TargetGridCell = cell;
				}
			}
		}
	}
}