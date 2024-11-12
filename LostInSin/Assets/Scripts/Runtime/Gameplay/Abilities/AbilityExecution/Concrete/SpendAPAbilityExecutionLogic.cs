using System;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Spend AP Ability Execution Logic",
		menuName = "LostInSin/Abilities/AbilityExecution/SpendAPAbility")]
	public class SpendAPAbilityExecutionLogic : AbilityExecutionLogic
	{
		public override void StartAction()
		{
			Debug.Log($"USE AP ACTION on user {_abilityRequestData.User.characterName}");
			EndAction();
		}

		public override void UpdateAction()
		{
			throw new Exception("This should never happen, this is an instant effect");
		}
	}
}