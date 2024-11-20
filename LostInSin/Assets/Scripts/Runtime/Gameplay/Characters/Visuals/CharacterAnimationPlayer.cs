using System;
using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Data;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Enums;
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

		private Dictionary<AnimationID, TransitionAssetBase> _characterAnimationClips;

		public void PlayAnimation(AnimationID animationID)
		{
			Debug.Log("CALLLED");

			Assert.IsNotNull(_characterAnimationClips[animationID],
			                 $"The animation clip with ID {animationID} was not found on the character with Avatar {_characterAvatar}.");

			TransitionAssetBase transitionAsset = _characterAnimationClips[animationID];
			AnimancerState state = _animancerComponent.Play(transitionAsset, fadeDuration: .25f);

			if (typeof(ClipTransitionSequence).IsAssignableFrom(transitionAsset.GetTransition().GetType()))
			{
				Debug.Log("ASDDDDDDD");
			}

			if (_characterAnimationClipsData.AnimationEventsLookup.TryGetValue(transitionAsset, out StringAsset stringAsset) && state.Events(this, out AnimancerEvent.Sequence events))
			{
				Debug.Log(events);
				events.SetCallback(stringAsset, OnEvent(stringAsset));
			}
		}

		private Action OnEvent(StringAsset value)
		{
			return () => UnityEngine.Debug.Log(value);
		}

		public void Start()
		{
			_characterAnimationClips = _characterAnimationClipsData.CharacterAnimations[_characterAvatar];
			PlayAnimation(AnimationID.Idle);
		}
	}
}