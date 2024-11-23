using Animancer;
using Cysharp.Threading.Tasks;
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
		public int BaseDamage;

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
				_arrow.Shoot(target: requestData.TargetCharacter);

				_direction = Quaternion.Euler(0, -90, 0) * _direction;
				requestData.User.PlayAnimation(AnimationID.Idle, 0.1f);
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			SlerpRotationTowardDirection();

			float dot = Quaternion.Dot(requestData.User.transform.rotation, Quaternion.LookRotation(_direction));

			if (_arrow.ReachedTarget && Mathf.Abs(dot) > 0.9999f) // if dot value is near 1, this means they are identical
			{
				requestData.TargetCharacter.TakeDamage(BaseDamage);
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


			_arrow.transform.SetParent(requestData.User.visualReferences.animationBones[AnimationBoneID.ArrowHand]);
			_arrow.ResetPosition();
		}
	}
}