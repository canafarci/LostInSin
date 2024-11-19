using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Shoot Arrow Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Shoot Arrow Ability")]
	public class ShootArrowAbilityExecutionLogic : AbilityExecutionLogic
	{
		public override void StartAbility()
		{
			_abilityRequestData.User.PlayAnimation(AnimationID.DrawArrow);
		}

		public override void UpdateAbility()
		{
			throw new System.NotImplementedException();
		}
	}
}