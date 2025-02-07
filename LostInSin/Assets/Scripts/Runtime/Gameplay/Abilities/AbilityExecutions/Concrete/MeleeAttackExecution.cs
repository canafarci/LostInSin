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
		public StringAsset TransitionToIdleTrigger;
		public int BaseDamage;

		private CharacterFacade _target;
		private bool _calledTakeDamage = false;

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

			SlerpRotationTowardDirection(_target.transform);

			// Wait for the melee hit animation event trigger
			if (!_calledTakeDamage && executionData.AbilityTriggers.Contains(MeleeHitTrigger))
			{
				_calledTakeDamage = true;
				// Apply damage
				_target.TakeDamage(BaseDamage);
			}

			if (executionData.AbilityTriggers.Contains(TransitionToIdleTrigger))
			{
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			// Return to idle
			executionData.User.PlayAnimation(AnimationID.Idle);

			// Mark ourselves as complete
			executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			_calledTakeDamage = false;
			_target = null;

			base.EndAbility();
		}
	}
}