using System;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityRequests.Concrete
{
	[CreateAssetMenu(fileName = "Spend AP Ability Request",
		menuName = "LostInSin/Abilities/AbilityRequests/SpendAPAbility")]
	public class SpendAPAbilityRequest : AbilityRequest
	{
		public override void StartRequest()
		{
			abilityRequestState = AbilityRequestState.Finished;
		}

		public override AbilityRequestState UpdateRequest()
		{
			throw new Exception("This should never happen.");
		}
	}
}