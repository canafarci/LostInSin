using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Shoot Arrow Ability")]
	public class ShootArrowAbilityExecution : AbilityExecution
	{
		public StringAsset ShootArrowTrigger;
		private Vector3 _direction;

		public override void StartAbility()
		{
			requestData.User.PlayAnimation(AnimationID.DrawArrow);
			executionStage = AbilityExecutionStage.Updating;

			_direction = (requestData.TargetCharacter.transform.position - requestData.User.transform.position).normalized;
			_direction.y = 0;

			_direction = Quaternion.Euler(0, 90, 0) * _direction;
		}

		public override void UpdateAbility()
		{
			SlerpRotationTowardDirection();

			if (executionData.AbilityTriggers.Contains(ShootArrowTrigger))
			{
				_direction = Quaternion.Euler(0, -90, 0) * _direction;
				requestData.User.PlayAnimation(AnimationID.Idle, 0.1f);
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			SlerpRotationTowardDirection();

			float dot = Quaternion.Dot(requestData.User.transform.rotation, Quaternion.LookRotation(_direction));
			if (Mathf.Abs(dot) > 0.9999f) // if dot value is near 1, this means they are identical
			{
				executionStage = AbilityExecutionStage.Complete;
			}
		}

		public override void EndAbility()
		{
			base.EndAbility();
		}

		private void SlerpRotationTowardDirection()
		{
			requestData.User.transform.rotation = Quaternion.Slerp(requestData.User.transform.rotation,
			                                                       Quaternion.LookRotation(_direction),
			                                                       AnimationConstants.rotationSpeed * Time.deltaTime);
		}
	}
}