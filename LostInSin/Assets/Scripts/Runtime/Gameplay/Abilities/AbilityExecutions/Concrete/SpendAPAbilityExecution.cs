using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Spend AP Ability Execution Logic", menuName = "LostInSin/Abilities/AbilityExecution/Spend AP Ability")]
	public class SpendAPAbilityExecution : AbilityExecution
	{
		private float _executionTime = 1f;
		private float _executionTimer;

		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);
			_executionTimer = 0f;
		}

		public override void StartAbility()
		{
			Debug.Log($"USE AP ACTION on user {executionData.User.characterName}");
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			_executionTimer += Time.deltaTime;

			if (_executionTimer >= _executionTime)
			{
				executionStage = AbilityExecutionStage.Complete;
			}
		}
	}
}