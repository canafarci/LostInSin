using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Fireball Ability Request",
	                 menuName = "LostInSin/Abilities/AbilityRequests/Fireball Ability")]
	public class FireballAbilityRequest : AbilityRequest
	{
		protected override void StartRequest()
		{
			state = AbilityRequestState.Continue;
		}

		public override void UpdateRequest()
		{
			if (IsRequestDataValid())
				state = AbilityRequestState.Complete;
		}

		private bool IsRequestDataValid()
		{
			return data.TargetGridCell != null &&
			       data.TargetGridCells != null;
		}
	}
}