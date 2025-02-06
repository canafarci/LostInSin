using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Abilities.AbilityRequests;
using LostInSin.Runtime.Gameplay.Characters;
using LostInSin.Runtime.Gameplay.Grid.Data;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Abilities.AbilityExecutions.Concrete
{
	[CreateAssetMenu(fileName = "Fireball Ability Execution Logic",
	                 menuName = "LostInSin/Abilities/AbilityExecution/Fireball Ability")]
	public class FireballAbilityExecution : AbilityExecution
	{
		public int Damage;
		private List<GridCell> _targets;

		public override void Initialize(AbilityRequestData data)
		{
			base.Initialize(data);

			_targets = data.TargetGridCells;
		}

		public override void StartAbility()
		{
			executionStage = AbilityExecutionStage.Updating;
		}

		public override void UpdateAbility()
		{
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

			executionStage = AbilityExecutionStage.Finishing;
		}

		public override void FinishAbility()
		{
			executionStage = AbilityExecutionStage.Complete;
		}

		public override void EndAbility()
		{
			_targets = null;

			base.EndAbility();
		}
	}
}