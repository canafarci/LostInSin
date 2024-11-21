using System.Collections.Generic;
using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Data;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Enums;
using LostInSin.Runtime.Gameplay.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals.Animations
{
	public class CharacterAnimationPlayer : IStartable, IInitializable
	{
		[Inject] private AnimancerComponent _animancerComponent;
		[Inject] private CharacterAnimationClipsData _characterAnimationClipsData;
		[Inject] private Avatar _characterAvatar;
		[Inject] private SignalBus _signalBus;

		private Dictionary<AnimationID, TransitionAssetBase> _characterAnimationClips;

		public void Initialize()
		{
			_characterAnimationClips = _characterAnimationClipsData.CharacterAnimations[_characterAvatar];

			foreach (StringAsset asset in _characterAnimationClipsData.AnimationEventStringAssets)
			{
				_animancerComponent.Events.AddTo<StringAsset>(asset, OnAnimationEvent);
			}
		}

		public void Start()
		{
			PlayAnimation(AnimationID.Idle);
		}

		public void PlayAnimation(AnimationID animationID, float crossFadeDuration = 0.25f)
		{
			Assert.IsNotNull(_characterAnimationClips[animationID],
			                 $"The animation clip with ID {animationID} was not found on the character with Avatar {_characterAvatar}.");

			TransitionAssetBase transitionAsset = _characterAnimationClips[animationID];
			AnimancerState state = _animancerComponent.Play(transitionAsset, fadeDuration: crossFadeDuration);
		}

		private void OnAnimationEvent(StringAsset value)
		{
			_signalBus.Fire(new AnimationEventSignal(value));
		}
	}
}