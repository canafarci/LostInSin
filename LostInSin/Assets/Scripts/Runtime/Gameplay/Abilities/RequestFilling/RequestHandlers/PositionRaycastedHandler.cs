using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class PositionRaycastedHandler : AbilityRequestTypeHandlerBase
	{
		[Inject] private PlayerAbilityRequestFiller _filler;
		[Inject] private PlayerRaycaster _raycaster;

		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.PositionRaycasted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest)
		{
			if (_filler.RaycastRequest != null && !_filler.RaycastRequest.isProcessed)
			{
				if (_raycaster.TryRaycastForPosition(abilityRequest,
				                                     ref _filler.RaycastRequest,
				                                     out Vector3 position))
				{
					abilityRequest.data.TargetPosition = position;
				}
			}
		}
	}
}