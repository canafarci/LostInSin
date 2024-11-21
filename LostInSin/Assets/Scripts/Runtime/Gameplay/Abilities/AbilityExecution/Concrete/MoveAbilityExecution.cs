using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Move Ability")]
	public class MoveAbilityExecution : AbilityExecution
	{
		private Vector3 _targetPosition;
		private Vector3 _targetDirection;
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

			CharacterFacade characterFacade = requestData.User;
			characterFacade.PlayAnimation(AnimationID.Move);
			characterFacade.SetCharacterCell(requestData.TargetGridCell);

			UpdatePositionAndDirection(characterFacade);
		}

		public override void UpdateAbility()
		{
			Transform userTransform = requestData.User.transform;

			if (Vector3.SqrMagnitude(userTransform.position - _targetPosition) > 0.01f)
			{
				userTransform.position += _targetDirection * AnimationConstants.movementSpeed * Time.deltaTime;
				userTransform.rotation = Quaternion.Slerp(userTransform.rotation,
				                                          Quaternion.LookRotation(_targetDirection),
				                                          AnimationConstants.rotationSpeed * Time.deltaTime);
			}
			else if (_positionIndex < requestData.PathCells.Count - 1)
			{
				_positionIndex++;
				UpdatePositionAndDirection(requestData.User);
			}
			else
			{
				executionStage = AbilityExecutionStage.Complete;
			}
		}

		public override void EndAbility()
		{
			requestData.User.PlayAnimation(AnimationID.Idle);
			base.EndAbility();
		}

		private void UpdatePositionAndDirection(CharacterFacade characterFacade)
		{
			_targetPosition = requestData.PathCells[_positionIndex].centerPosition;
			_targetDirection = (_targetPosition - characterFacade.transform.position).normalized;
		}
	}
}