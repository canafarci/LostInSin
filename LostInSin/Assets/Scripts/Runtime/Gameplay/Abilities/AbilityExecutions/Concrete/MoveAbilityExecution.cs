using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Move Ability")]
	public class MoveAbilityExecution : AbilityExecution
	{
		private Vector3 _targetPosition;
		private Vector3 _targetDirection;
		private int _positionIndex;

		private GridCell _targetGridCell;
		private List<GridCell> _pathCells;

		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);

			_targetPosition = default;
			_positionIndex = 0;
			_targetGridCell = data.TargetGridCell;
			_pathCells = data.PathCells;
		}

		public override void StartAbility()
		{
			executionData.User.PlayAnimation(AnimationID.Move);
			executionData.User.SetCharacterCell(_targetGridCell);

			UpdatePositionAndDirection(executionData.User);

			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			Transform userTransform = executionData.User.transform;

			if (Vector3.SqrMagnitude(userTransform.position - _targetPosition) > 0.01f)
			{
				userTransform.position += _targetDirection * AnimationConstants.movementSpeed * Time.deltaTime;
				userTransform.rotation = Quaternion.Slerp(userTransform.rotation,
				                                          Quaternion.LookRotation(_targetDirection),
				                                          AnimationConstants.rotationSpeed * Time.deltaTime
				                                         );
			}
			else if (_positionIndex < _pathCells.Count - 1)
			{
				_positionIndex++;
				UpdatePositionAndDirection(executionData.User);
			}
			else
			{
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			executionStage = AbilityExecutionStage.Complete;
			executionData.User.PlayAnimation(AnimationID.Idle);
		}

		private void UpdatePositionAndDirection(CharacterFacade characterFacade)
		{
			_targetPosition = _pathCells[_positionIndex].centerPosition;
			_targetDirection = (_targetPosition - characterFacade.transform.position).normalized;
		}

		public override void EndAbility()
		{
			_targetGridCell = null;
			_pathCells = null;

			base.EndAbility();
		}
	}
}