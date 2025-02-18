using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterVisualReferences : MonoBehaviour
	{
		[Inject] private CharacterData _characterData;

		[SerializeField] private Transform ProjectileHitPoint;
		[SerializeField] private GameObject ActiveCharacterDecal;
		[SerializeField] private TextMeshProUGUI HealthText;
		[SerializeField] private Image HealthBarFillImage;

		private Dictionary<AnimationBoneID, Transform> _animationBones = new();

		public Sprite characterPortrait { get; private set; }
		public Transform projectileHitPoint => ProjectileHitPoint;
		public Dictionary<AnimationBoneID, Transform> animationBones => _animationBones;
		public GameObject activeCharacterDecal => ActiveCharacterDecal;
		public TextMeshProUGUI healthText => HealthText;
		public Image healthBarFillImage => HealthBarFillImage;

		private void Awake()
		{
			characterPortrait = _characterData.CharacterPortrait;
			InitializeBoneLookup();
		}

		private void InitializeBoneLookup()
		{
			foreach (AnimationBone bone in transform.GetComponentsInChildren<AnimationBone>())
			{
				_animationBones[bone.BoneID] = bone.transform;
			}
		}
	}
}