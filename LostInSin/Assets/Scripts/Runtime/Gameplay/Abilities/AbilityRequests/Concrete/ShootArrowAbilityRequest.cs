using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Request",
	                 menuName = "LostInSin/Abilities/AbilityRequests/Shoot Arrow Ability")]
	public class ShootArrowAbilityRequest : AbilityRequest
	{
		protected override void StartRequest()
		{
			state = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (data.Target != null)
			{
				state = AbilityRequestState.Complete;
			}
		}
	}
}