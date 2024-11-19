using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Data;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals
{
	public class CharacterAnimationPlayer : IStartable
	{
		[Inject] private AnimancerComponent _animancerComponent;
		[Inject] private CharacterAnimationClipsData _characterAnimationClipsData;
		[Inject] private Avatar _characterAvatar;

		private Dictionary<AnimationID, AnimationClip> _characterAnimationClips;

		public void PlayAnimation(AnimationID animationID)
		{
			Assert.IsNotNull(_characterAnimationClips[animationID],
			                 $"The animation clip with ID {animationID} was not found on the character with Avatar {_characterAvatar}.");

			AnimationClip animationClip = _characterAnimationClips[animationID];
			_animancerComponent.Play(animationClip, fadeDuration: .25f);
		}

		public void Start()
		{
			_characterAnimationClips = _characterAnimationClipsData.CharacterAnimations[_characterAvatar];
			_animancerComponent.Play(_characterAnimationClips[AnimationID.Idle]);
		}
	}
}