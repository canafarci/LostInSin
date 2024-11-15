using System;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Spend AP Ability Execution Logic",
		menuName = "LostInSin/Abilities/AbilityExecution/SpendAPAbility")]
	public class SpendAPAbilityExecutionLogic : AbilityExecutionLogic
	{
		private float _executionTime = 1f;
		private float _executionTimer;

		public override void Initialize(AbilityRequestData requestData)
		{
			base.Initialize(requestData);
			_executionTimer = 0f;
		}

		public override void StartAbility()
		{
			Debug.Log($"USE AP ACTION on user {abilityRequestData.User.characterName}");
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			_executionTimer += Time.deltaTime;

			if (_executionTimer >= _executionTime)
			{
				EndAbility();
			}
		}
	}
}