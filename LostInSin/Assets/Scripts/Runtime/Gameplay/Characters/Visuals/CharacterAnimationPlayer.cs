using Animancer;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Data;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Gameplay.Characters.Visuals
{
	public class CharacterAnimationPlayer : SignalListener, IStartable
	{
		[Inject] private AnimancerComponent _animancerComponent;
		[Inject] private CharacterAnimationData _characterAnimationData;
		[Inject] private Avatar _characterAvatar;

		private CharacterAnimationClips _characterAnimationClips;

		protected override void SubscribeToEvents()
		{
		}

		public void Start()
		{
			_characterAnimationClips = _characterAnimationData.CharacterAnimations[_characterAvatar];
			_animancerComponent.Play(_characterAnimationClips.IdleAnimation);
		}

		protected override void UnsubscribeFromEvents()
		{
		}
	}
}