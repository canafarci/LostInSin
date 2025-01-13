using Animancer;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Melee Attack Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Melee Attack Ability")]
	public class MeleeAttackExecution : AbilityExecution
	{
		public StringAsset MeleeHitTrigger;
		public int BaseDamage;

		private CharacterFacade _target;

		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);

			_target = data.TargetCharacter;
		}

		public override void StartAbility()
		{
			// Start the attack animation
			executionData.User.PlayAnimation(AnimationID.MeleeAttack);

			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			// Called each frame while in the "Updating" stage

			// Optionally Slerp rotation toward target
			SlerpRotationTowardDirection();

			// Wait for the melee hit animation event trigger
			if (executionData.AbilityTriggers.Contains(MeleeHitTrigger))
			{
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			// Apply damage
			_target.TakeDamage(BaseDamage);

			// Return to idle
			executionData.User.PlayAnimation(AnimationID.Idle, 0.1f);

			// Mark ourselves as complete
			executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			_target = null;

			base.EndAbility();
		}

		private void SlerpRotationTowardDirection()
		{
			if (_target == null) return;
			Vector3 direction = (_target.transform.position - executionData.User.transform.position).normalized;

			executionData.User.transform.rotation = Quaternion.Slerp(executionData.User.transform.rotation,
			                                                         Quaternion.LookRotation(direction),
			                                                         AnimationConstants.rotationSpeed * Time.deltaTime);
		}
	}
}