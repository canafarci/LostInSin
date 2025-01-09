using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Infrastructure.EnumFieldAdder;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Data
{
	[CreateAssetMenu(fileName = "Character Animation Lookup",
	                 menuName = "LostInSin/Animations/Character Animation Clips Data")]
	public class CharacterAnimationClipsData : SerializedScriptableObject
	{
		public Dictionary<Avatar, Dictionary<AnimationID, TransitionAssetBase>> CharacterAnimations;
		public List<StringAsset> AnimationEventStringAssets;

		[ShowInInspector] private EnumFieldAdder<AnimationID> _animationIDAdder = new();
	}
}