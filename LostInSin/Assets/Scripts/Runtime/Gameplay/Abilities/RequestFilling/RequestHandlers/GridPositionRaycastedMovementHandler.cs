using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Grid.Data;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class GridPositionRaycastedMovementHandler : AbilityRequestTypeHandlerBase
	{
		[Inject] private PlayerAbilityRequestFiller _filler;
		[Inject] private PlayerRaycaster _raycaster;

		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.GridPositionRaycasted) && requestType.HasFlag(AbilityRequestType.Movement);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest)
		{
			// Do a raycast for an empty cell, set it as TargetGridCell
			if (_filler.RaycastRequest != null && !_filler.RaycastRequest.isProcessed)
			{
				if (_raycaster.TryRaycastForEmptyGridCell(abilityRequest,
				                                          ref _filler.RaycastRequest,
				                                          out GridCell cell))
				{
					abilityRequest.data.TargetGridCell = cell;
				}
			}
		}
	}
}