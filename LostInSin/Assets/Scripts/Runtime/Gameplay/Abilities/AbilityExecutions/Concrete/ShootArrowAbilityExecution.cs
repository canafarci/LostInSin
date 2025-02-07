using Animancer;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Abilities.Projectiles;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Shoot Arrow Ability")]
	public class ShootArrowAbilityExecution : AbilityExecution
	{
		public StringAsset ShootArrowTrigger;
		public int BaseDamage;

		private Vector3 _direction;
		private Arrow _arrow;

		private CharacterFacade _target;


		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);

			_target = data.TargetCharacter;
		}

		public override void StartAbility()
		{
			executionData.User.PlayAnimation(AnimationID.DrawArrow);

			GetArrowFromPool();

			SetCharacterDirection();

			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			SlerpRotationTowardDirection(_direction);

			if (executionData.AbilityTriggers.Contains(ShootArrowTrigger))
			{
				executionStage = AbilityExecutionStage.Finishing;

				_arrow.Shoot(target: _target);
				_direction = Quaternion.Euler(0, -90, 0) * _direction;
				executionData.User.PlayAnimation(AnimationID.Idle, 0.1f);
			}
		}

		public override void FinishAbility()
		{
			SlerpRotationTowardDirection(_direction);

			float dot = Quaternion.Dot(executionData.User.transform.rotation, Quaternion.LookRotation(_direction));

			if (_arrow.ReachedTarget && Mathf.Abs(dot) > 0.9999f) // if dot value is near 1, this means they are identical
			{
				_target.TakeDamage(BaseDamage);
				executionStage = AbilityExecutionStage.Complete;
			}
		}

		private void SetCharacterDirection()
		{
			_direction = (_target.transform.position - executionData.User.transform.position).normalized;
			_direction.y = 0;
			_direction = Quaternion.Euler(0, 90, 0) * _direction;
		}

		private void GetArrowFromPool()
		{
			_arrow = PoolManager.GetMono<Arrow>();


			_arrow.transform.SetParent(executionData.User.visualReferences.animationBones[AnimationBoneID.ArrowHand]);
			_arrow.ResetPosition();
		}

		public override void EndAbility()
		{
			_target = null;

			base.EndAbility();
		}
	}
}