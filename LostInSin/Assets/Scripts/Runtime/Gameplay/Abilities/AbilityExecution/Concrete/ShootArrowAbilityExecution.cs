using Animancer;
using LostInSin.Runtime.Gameplay.Abilities.Projectiles;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Shoot Arrow Ability")]
	public class ShootArrowAbilityExecution : AbilityExecution
	{
		public StringAsset ShootArrowTrigger;
		private Vector3 _direction;
		private Arrow _arrow;

		public override void StartAbility()
		{
			requestData.User.PlayAnimation(AnimationID.DrawArrow);

			GetArrowFromPool();

			SetCharacterDirection();

			executionStage = AbilityExecutionStage.Updating;
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

		private void SlerpRotationTowardDirection()
		{
			requestData.User.transform.rotation = Quaternion.Slerp(requestData.User.transform.rotation,
			                                                       Quaternion.LookRotation(_direction),
			                                                       AnimationConstants.rotationSpeed * Time.deltaTime);
		}

		private void SetCharacterDirection()
		{
			_direction = (requestData.TargetCharacter.transform.position - requestData.User.transform.position).normalized;
			_direction.y = 0;
			_direction = Quaternion.Euler(0, 90, 0) * _direction;
		}
		
		private void GetArrowFromPool()
		{
			_arrow = PoolManager.GetMono<Arrow>();
			
			Vector3 originalPos = _arrow.transform.position;
			Quaternion originalRot = _arrow.transform.rotation;
			Vector3 originalScale = _arrow.transform.lossyScale;
			
			_arrow.transform.SetParent(requestData.User.animationBones[AnimationBoneID.ArrowHand], false);
			
			_arrow.transform.localRotation = originalRot;
			_arrow.transform.localPosition = originalPos;
			_arrow.transform.localScale = originalScale;
		}
	}
}