using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Execution Logic",
		menuName = "LostInSin/Abilities/AbilityExecution/Move Ability")]
	public class MoveAbilityExecutionLogic : AbilityExecutionLogic
	{
		private Vector3 _targetPosition;
		private int _positionIndex;

		public override void Initialize(AbilityRequestData requestData)
		{
			base.Initialize(requestData);

			_targetPosition = default;
			_positionIndex = 0;
		}

		public override void StartAbility()
		{
			executionStage = AbilityExecutionStage.Updating;
			_targetPosition = _abilityRequestData.PathCells[_positionIndex].centerPosition;
		}

		public override void UpdateAbility()
		{
			Transform userTransform = _abilityRequestData.User.transform;

			if (Vector3.SqrMagnitude(userTransform.position - _targetPosition) > 0.01f)
			{
				Vector3 direction = _targetPosition - userTransform.position;
				userTransform.position += direction.normalized * 5f * Time.deltaTime;
			}

			else if (_positionIndex < _abilityRequestData.PathCells.Count - 1)
			{
				_targetPosition = _abilityRequestData.PathCells[++_positionIndex].centerPosition;
			}
			else
			{
				EndAbility();
			}
		}
	}
}