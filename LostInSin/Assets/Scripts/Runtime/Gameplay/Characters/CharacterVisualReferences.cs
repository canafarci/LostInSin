using System.Collections.Generic;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters
{
	public class CharacterVisualReferences : MonoBehaviour
	{
		private Dictionary<AnimationBoneID, Transform> _animationBones = new();
		public Transform ProjectileHitPoint;

		public Dictionary<AnimationBoneID, Transform> animationBones => _animationBones;

		private void Awake()
		{
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