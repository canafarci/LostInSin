using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Melee Attack Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Melee Attack Ability")]
	public class MeleeAttackExecution : AbilityExecution
	{
		public MoveAbilityExecution MoveAbilityExecution;
		public StringAsset MeleeHitTrigger;
		public int BaseDamage;

		private bool _calledPlayAnimation = false;

		public override void StartAbility()
		{
			requestData.PathCells.RemoveAt(requestData.PathCells.Count - 1);
			requestData.TargetGridCell = requestData.PathCells[^1];
			base.Initialize(requestData);

			MoveAbilityExecution.Initialize(requestData);
			MoveAbilityExecution.StartAbility();

			_calledPlayAnimation = false;
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			if (MoveAbilityExecution.executionStage != AbilityExecutionStage.Finishing)
			{
				MoveAbilityExecution.UpdateAbility();
			}
			else
			{
				if (!_calledPlayAnimation)
				{
					_calledPlayAnimation = true;
					requestData.User.PlayAnimation(AnimationID.MeleeAttack);
				}

				SlerpRotationTowardDirection();
			}

			if (executionData.AbilityTriggers.Contains(MeleeHitTrigger))
			{
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		public override void FinishAbility()
		{
			requestData.User.PlayAnimation(AnimationID.Idle, 0.1f);

			requestData.TargetCharacter.TakeDamage(BaseDamage);
			executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			MoveAbilityExecution.EndAbility();
			base.EndAbility();
		}

		private void SlerpRotationTowardDirection()
		{
			Vector3 direction = (requestData.TargetCharacter.transform.position - requestData.User.transform.position).normalized;

			requestData.User.transform.rotation = Quaternion.Slerp(requestData.User.transform.rotation,
			                                                       Quaternion.LookRotation(direction),
			                                                       AnimationConstants.rotationSpeed * Time.deltaTime);
		}
	}
}