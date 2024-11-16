using System;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Spend AP Ability Request",
		menuName = "LostInSin/Abilities/AbilityRequests/Spend AP Ability")]
	public class SpendAPAbilityRequest : AbilityRequest
	{
		public override void StartRequest()
		{
			state = AbilityRequestState.Complete;
		}

		public override void UpdateRequest()
		{
			throw new Exception("This should never happen.");
		}
	}
}