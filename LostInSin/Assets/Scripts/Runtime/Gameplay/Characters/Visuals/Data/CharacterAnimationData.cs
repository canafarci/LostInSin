using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Data
{
	[CreateAssetMenu(fileName = "Character Animation Lookup", menuName = "LostInSin/Animations/Character Animation Data")]
	public class CharacterAnimationData : SerializedScriptableObject
	{
		public Dictionary<Avatar, CharacterAnimationClips> CharacterAnimations = new();
	}

	public struct CharacterAnimationClips
	{
		public AnimationClip IdleAnimation;
	}
}