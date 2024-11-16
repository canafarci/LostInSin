using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using LostInSin.Runtime.Gameplay.Characters;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecution.Concrete
{
	[CreateAssetMenu(fileName = "Move Ability Execution Logic",
		menuName = "LostInSin/Abilities/AbilityExecution/Move Ability")]
	public class MoveAbilityExecutionLogic : AbilityExecutionLogic
	{
		private TweenerCore<Vector3, Vector3, VectorOptions> _moveTween;

		public override void StartAbility()
		{
			CharacterFacade character = _abilityRequestData.User;

			_moveTween = character.transform.DOMove(_abilityRequestData.TargetGridCell.centerPosition, 4f)
				.SetSpeedBased();

			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			if (!_moveTween.active)
			{
				EndAbility();
			}
		}

		protected override void EndAbility()
		{
			_moveTween = null;
			base.EndAbility();
		}
	}
}