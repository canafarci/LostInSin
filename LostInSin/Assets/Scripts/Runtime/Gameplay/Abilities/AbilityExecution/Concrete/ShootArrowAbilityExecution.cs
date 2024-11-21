using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Shoot Arrow Ability")]
	public class ShootArrowAbilityExecution : AbilityExecution
	{
		public StringAsset ShootArrowTrigger;

		public override void StartAbility()
		{
			requestData.User.PlayAnimation(AnimationID.DrawArrow);
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			if (executionData.AbilityTriggers.Contains(ShootArrowTrigger))
				executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			requestData.User.PlayAnimation(AnimationID.Idle, 0.1f);
			base.EndAbility();
		}
	}
}