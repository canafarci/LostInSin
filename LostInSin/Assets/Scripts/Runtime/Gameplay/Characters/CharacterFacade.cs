using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using VContainer;
using LostInSin.Runtime.Gameplay.Abilities;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Grid.Data;
using LostInSin.Runtime.Infrastructure.Signals;
using Sirenix.Serialization;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterFacade : SerializedMonoBehaviour
	{
		[Inject] private Character _character;
		[Inject] private SignalBus _signalBus;
		[Inject] private CharacterAnimationPlayer _characterAnimationPlayer;

		private Dictionary<AnimationBoneID, Transform> _animationBones = new();

		public bool isPlayerCharacter => _character.characterData.IsPlayerCharacter;
		public List<Ability> abilities => _character.characterData.Abilities;
		public int actionPoints => _character.currentActionPoints;
		public int initiative => _character.initiative;
		public string characterName => _character.characterName;
		public GridCell currentCell => _character.currentCell;

		public Dictionary<AnimationBoneID, Transform> animationBones => _animationBones;
		public Transform ProjectileHitPoint;

		private void Awake()
		{
			InitializeBoneLookup();
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
			_character.ResetActionPoints();
		}

		public void ReduceActionPoints(int abilityActionPointCost)
		{
			_character.UseActionPoints(abilityActionPointCost);
			_signalBus.Fire(new CharacterAPChangedSignal());
		}

		public void PlayAnimation(AnimationID animationID, float crossfadeDuration = 0.25f) => _characterAnimationPlayer.PlayAnimation(animationID, crossfadeDuration);

		private void InitializeBoneLookup()
		{
			foreach (AnimationBone bone in transform.GetComponentsInChildren<AnimationBone>())
			{
				_animationBones[bone.BoneID] = bone.transform;
			}
		}
	}
}