using System.Collections.Generic;
using System.Runtime.InteropServices;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : MonoBehaviour
	{
		[Inject] private Character _character;
		[Inject] private SignalBus _signalBus;
		[Inject] private CharacterAnimationPlayer _characterAnimationPlayer;

		public bool isPlayerCharacter => _character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => _character.characterData.Abilities;
		public int actionPoints => _character.currentActionPoints;
		public int initiative => _character.initiative;
		public string characterName => _character.characterName;
		public GridCell currentCell => _character.currentCell;

		public void SetCharacterCell(GridCell cell, bool warp = false)
		{
			_character.currentCell?.SetAsUnoccupied();

			_character.currentCell = cell;
			cell.SetAsOccupied();

			if (warp)
			{
				transform.position = cell.centerPosition;
			}
		}

		public void SetAsActiveCharacter()
		{
			UnityEngine.Debug.Log($"Is Active {_character.characterName}");
			_character.ResetActionPoints();
		}

		public void ReduceActionPoints(int abilityActionPointCost)
		{
			_character.UseActionPoints(abilityActionPointCost);
			_signalBus.Fire(new CharacterAPChangedSignal());
		}

		public void PlayAnimation(AnimationID animationID) => _characterAnimationPlayer.PlayAnimation(animationID);
	}
}