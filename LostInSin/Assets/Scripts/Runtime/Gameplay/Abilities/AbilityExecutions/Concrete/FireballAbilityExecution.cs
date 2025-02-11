using System;
using System.Collections.Generic;
using Animancer;
using Cysharp.Threading.Tasks;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Visuals.FX;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Fireball Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Fireball Ability")]
	public class FireballAbilityExecution : AbilityExecution
	{
		public int Damage;
		public StringAsset MagicCastTrigger;
		public StringAsset TransitionToIdleTrigger;

		private List<GridCell> _targets;
		private Vector3 _targetPosition;

		private bool _calledTakeDamage = false;


		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);

			_targets = data.TargetGridCells;
			_targetPosition = data.TargetGridCell.centerPosition;
		}

		public override void StartAbility()
		{
			executionData.User.PlayAnimation(AnimationID.MagicCast);

			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
			SlerpTowardsFacePosition(_targetPosition);

			// Wait for the melee hit animation event trigger
			if (!_calledTakeDamage && executionData.AbilityTriggers.Contains(MagicCastTrigger))
			{
				SpawnFireballVisual();

				// Apply damage
				_calledTakeDamage = true;
				DamageCharactersInArea().Forget();
			}

			if (executionData.AbilityTriggers.Contains(TransitionToIdleTrigger))
			{
				executionStage = AbilityExecutionStage.Finishing;
			}
		}

		private void SpawnFireballVisual()
		{
			FireballFX fx = PoolManager.GetMono<FireballFX>();
			fx.SetPosition(_targetPosition);
			fx.Play().Forget();
		}

		private async UniTask DamageCharactersInArea()
		{
			await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

			List<CharacterFacade> targetCharacters = new();

			foreach (GridCell cell in _targets)
			{
				if (cell.isOccupied && cell.character.teamID != executionData.User.teamID)
				{
					targetCharacters.Add(cell.character);
				}
			}

			foreach (CharacterFacade character in targetCharacters)
			{
				character.TakeDamage(Damage);
			}
		}

		public override void FinishAbility()
		{
			// Return to idle
			executionData.User.PlayAnimation(AnimationID.Idle);

			executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			_calledTakeDamage = false;
			_targets = null;
			_targetPosition = default;

			base.EndAbility();
		}
	}
}