using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.RequestFilling.RequestHandlers
{
	public class SelfTargetedHandler : AbilityRequestTypeHandlerBase
	{
		public override bool AppliesTo(AbilityRequestType requestType)
		{
			return requestType.HasFlag(AbilityRequestType.SelfTargeted);
		}

		protected override void ProcessRequest(AbilityRequest abilityRequest, PlayerAbilityRequestFiller context)
		{
			// e.g., set the target to the user itself
			abilityRequest.data.TargetCharacter ??= abilityRequest.data.User;
		}
	}
}