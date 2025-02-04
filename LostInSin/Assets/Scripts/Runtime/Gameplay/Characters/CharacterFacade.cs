using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using VContainer;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Grid.Data;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using Sirenix.Serialization;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : MonoBehaviour
	{
		[Inject] private Character _character;
		[Inject] private SignalBus _signalBus;
		[Inject] private CharacterAnimationPlayer _characterAnimationPlayer;
		[Inject] private CharacterVisualReferences _characterVisualReferences;

		public bool isPlayerCharacter => _character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => _character.characterData.Abilities;

		public int actionPoints => _character.currentStats[StatID.ActionPoint];
		public int initiative => _character.currentStats[StatID.Initiative];

		public string characterName => _character.characterName;
		public GridCell currentCell => _character.currentCell;
		public CharacterVisualReferences visualReferences => _characterVisualReferences;
		public bool isDead => _character.currentStats[StatID.Health] <= 0;

		public void TakeDamage(int change)
		{
			int currentHealth = _character.currentStats[StatID.Health];
			currentHealth -= change;

			if (currentHealth <= 0)
			{
				OnCharacterDeath();
			}
			else
			{
				_character.currentStats[StatID.Health] = currentHealth;
			}

			Debug.Log($"{name} hp: {currentHealth}");
		}

		private void OnCharacterDeath()
		{
			_character.currentStats[StatID.Health] = 0;
			PlayAnimation(AnimationID.Die);
			_signalBus.Fire(new CharacterDiedSignal(this));
			currentCell.SetAsUnoccupied();
		}

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
			_character.currentStats[StatID.ActionPoint] = _character.maxStats[StatID.ActionPoint];

			visualReferences.activeCharacterDecal.SetActive(true);
		}

		public void SetCharacterAsInactive()
		{
			visualReferences.activeCharacterDecal.SetActive(false);
		}

		public void ReduceActionPoints(int abilityActionPointCost)
		{
			_character.currentStats[StatID.ActionPoint] -= abilityActionPointCost;
			_signalBus.Fire(new CharacterAPChangedSignal());
		}

		public void PlayAnimation(AnimationID animationID, float crossfadeDuration = 0.25f) => _characterAnimationPlayer.PlayAnimation(animationID, crossfadeDuration);
	}
}