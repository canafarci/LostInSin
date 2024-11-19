using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Data
{
	[CreateAssetMenu(fileName = "Character Animation Lookup",
	                 menuName = "LostInSin/Animations/Character Animation Clips Data")]
	public class CharacterAnimationClipsData : SerializedScriptableObject
	{
		public Dictionary<Avatar, Dictionary<AnimationID, TransitionAssetBase>> CharacterAnimations;
	}
}